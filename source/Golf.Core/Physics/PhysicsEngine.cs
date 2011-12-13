using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;
using Golf.Core.Maths;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        readonly IEventTriggerer _eventTriggerer;

        readonly ICollection<PhysicsObject> _physicsObjects = new List<PhysicsObject>();

        public PhysicsEngine(IObservable<IGameEvent> events, IEventTriggerer eventTriggerer) {
            _eventTriggerer = eventTriggerer;

            events
                .OfType<AddGameObjectRequest>()
                .Subscribe(AddGameObject);


            events
                .OfType<PositionChangeRequest>()
                .Subscribe(ChangePosition);

            events
                .OfType<ApplyImpulseRequest>()
                .Subscribe(ApplyImpulse);

            events.OfType<AddForceRequest>()
                .Subscribe(AddForce);

            events.OfType<RemoveForceRequest>()
                .Subscribe(RemoveForce);
        }

        #region IPhysicsEngine Members

        public void Tick(TimeSpan tickPeriod) {
            foreach (var physicsObject in _physicsObjects) {
                physicsObject.DynamicBody.Position +=
                    physicsObject.DynamicBody.Velocity*tickPeriod.TotalSeconds;


                physicsObject.DynamicBody.Velocity = CalculateVelocity(physicsObject.DynamicBody, tickPeriod);
            }
            _eventTriggerer.Trigger(new Tick());
        }

        #endregion

        Vector2 CalculateVelocity(DynamicBody body, TimeSpan tickPeriod) {
            var impulse = body.Forces.Aggregate(Vector2.Zero,
                                                (current, force) =>
                                                current + force.CalculateForce(body))
                                                * tickPeriod.TotalSeconds;

            var velocityBeforeResistance = body.Velocity + impulse;

            var resistiveImpulse = body.ResistiveForces.Aggregate(Vector2.Zero,
                                                                  (c, f) =>
                                                                  c +
                                                                  f.CalculateForce(body))
                                                                  * tickPeriod.TotalSeconds;

            return new Vector2(
                AddStickingToZero(velocityBeforeResistance.X, resistiveImpulse.X),
                AddStickingToZero(velocityBeforeResistance.Y, resistiveImpulse.Y));
        }

        double AddStickingToZero(double v1, double v2) {
            var sum = v1 + v2;

            if (Math.Sign(v1) != Math.Sign(sum))
                return 0.0;

            return sum;
        }

        void AddGameObject(AddGameObjectRequest message) {
            var dynamicBody = new DynamicBody {Position = message.Position};
            message.GameObject.Body = dynamicBody;
            _physicsObjects.Add(new PhysicsObject(message.GameObject, dynamicBody));
            _eventTriggerer.Trigger(new GameObjectAdded(message.GameObject));
            _eventTriggerer.Trigger(new PositionChanged(message.GameObject));
        }

        void ChangePosition(PositionChangeRequest message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Position = message.Position;
            _eventTriggerer.Trigger(new PositionChanged(message.GameObject));
        }

        void ApplyImpulse(ApplyImpulseRequest message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Velocity += message.Impulse;
        }

        void AddForce(AddForceRequest e) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == e.GameObject).Single();

            if (e.Force is IResistiveForce)
                physicsObject.DynamicBody.ResistiveForces.Add(e.Force);
            else
                physicsObject.DynamicBody.Forces.Add(e.Force);
        }

        void RemoveForce(RemoveForceRequest e) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == e.GameObject).Single();

            if (e.Force is IResistiveForce)
                physicsObject.DynamicBody.ResistiveForces.Remove(e.Force);
            else
                physicsObject.DynamicBody.Forces.Remove(e.Force);
        }
    }
}