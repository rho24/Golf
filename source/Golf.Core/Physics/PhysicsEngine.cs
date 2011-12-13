using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;
using Golf.Core.Maths;
using Golf.Core.Physics.Barriers;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        readonly IBarriers _barriers;
        readonly IEventTriggerer _eventTriggerer;

        readonly ICollection<PhysicsObject> _physicsObjects = new List<PhysicsObject>();

        public PhysicsEngine(IObservable<IGameEvent> events, IEventTriggerer eventTriggerer, IBarriers barriers) {
            _eventTriggerer = eventTriggerer;
            _barriers = barriers;

            events.OfType<AddGameObjectRequest>()
                .Subscribe(AddGameObject);

            events.OfType<PositionChangeRequest>()
                .Subscribe(ChangePosition);

            events.OfType<ApplyImpulseRequest>()
                .Subscribe(ApplyImpulse);

            events.OfType<AddForceRequest>()
                .Subscribe(AddForce);

            events.OfType<RemoveForceRequest>()
                .Subscribe(RemoveForce);
        }

        #region IPhysicsEngine Members

        public bool IsInRest {
            get { return _physicsObjects.All(p => p.Body.IsInRest); }
        }

        public void Tick(TimeSpan tickPeriod) {
            var collision = (from o in _physicsObjects
                             from b in _barriers
                             let c = b.CalculateCollision(o.GameObject, tickPeriod)
                             where c != null
                             select c).FirstOrDefault();

            if (collision != null) {
                UpdatePositions(collision.CollisionTime);
                collision.Apply(_eventTriggerer);
                Tick(tickPeriod - collision.CollisionTime);
                return;
            }

            UpdatePositions(tickPeriod);

            UpdateVelocities(tickPeriod);

            _eventTriggerer.Trigger(new Tick());
        }

        #endregion

        void UpdatePositions(TimeSpan tickPeriod) {
            foreach (var physicsObject in _physicsObjects) {
                physicsObject.Body.Position +=
                    physicsObject.Body.Velocity*tickPeriod.TotalSeconds;
            }
        }

        void UpdateVelocities(TimeSpan tickPeriod) {
            foreach (var physicsObject in _physicsObjects) {
                physicsObject.Body.Position +=
                    physicsObject.Body.Velocity*tickPeriod.TotalSeconds;

                var velocityResult = CalculateVelocity(physicsObject.Body, tickPeriod);
                physicsObject.Body.Velocity = velocityResult.Velocity;
                physicsObject.Body.IsInRest = velocityResult.IsInRest;
            }
        }

        VelocityResult CalculateVelocity
            (DynamicBody body, TimeSpan tickPeriod) {
            var impulse = body.Forces.Aggregate(Vector2.Zero,
                                                (current, force) =>
                                                current + force.CalculateForce(body))
                          *tickPeriod.TotalSeconds; //TODO: requires mass;

            var velocityBeforeResistance = body.Velocity + impulse;

            var resistiveImpulse = body.ResistiveForces.Aggregate(Vector2.Zero,
                                                                  (c, f) =>
                                                                  c +
                                                                  f.CalculateForce(body))
                                   *tickPeriod.TotalSeconds;

            var velocity = new Vector2(
                AddStickingToZero(velocityBeforeResistance.X, resistiveImpulse.X),
                AddStickingToZero(velocityBeforeResistance.Y, resistiveImpulse.Y));

            var isInRest = (velocity == Vector2.Zero && resistiveImpulse.Length > impulse.Length);

            return new VelocityResult(velocity, isInRest);
        }

        double AddStickingToZero
            (double v1, double v2) {
            var sum = v1 + v2;

            if (Math.Sign(v1) != Math.Sign(sum))
                return 0.0;

            return sum;
        }

        void AddGameObject
            (AddGameObjectRequest
                 message) {
            var dynamicBody = new DynamicBody {Position = message.Position};
            message.GameObject.Body = dynamicBody;
            _physicsObjects.Add(new PhysicsObject(message.GameObject, dynamicBody));
            _eventTriggerer.Trigger(new GameObjectAdded(message.GameObject));
            _eventTriggerer.Trigger(new PositionChanged(message.GameObject));
        }

        void ChangePosition
            (PositionChangeRequest
                 message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.Body.Position = message.Position;
            _eventTriggerer.Trigger(new PositionChanged(message.GameObject));
        }

        void ApplyImpulse
            (ApplyImpulseRequest
                 message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.Body.Velocity += message.Impulse; //TODO: requires mass;
        }

        void AddForce
            (AddForceRequest
                 e) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == e.GameObject).Single();

            if (e.Force is IResistiveForce)
                physicsObject.Body.ResistiveForces.Add(e.Force);
            else
                physicsObject.Body.Forces.Add(e.Force);
        }

        void RemoveForce
            (RemoveForceRequest
                 e) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == e.GameObject).Single();

            if (e.Force is IResistiveForce)
                physicsObject.Body.ResistiveForces.Remove(e.Force);
            else
                physicsObject.Body.Forces.Remove(e.Force);
        }
    }
}