using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;
using Golf.Core.Maths;
using Golf.Core.Physics.Barriers;
using Golf.Core.Physics.Collisions;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        const int MaxCollisionDepth = 5;
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

        public void Tick(TickTime tickTime) {
            var collision = (from o in _physicsObjects
                             from b in _barriers
                             let c = CalculateCollision(b, o.Body, TimeSpan.Zero, tickTime.TickElapsed, 0)
                             where c != null
                             orderby c.CollisionTime
                             select c).FirstOrDefault();

            if (collision != null) {
                UpdateKinematics(collision.CollisionTime);
                collision.Barrier.ApplyCollision(_eventTriggerer, collision.Body);
                Tick(new TickTime(tickTime.TickElapsed - collision.CollisionTime, tickTime.TotalElapsed));
                return;
            }

            UpdateKinematics(tickTime.TickElapsed);

            _eventTriggerer.Trigger(new Tick());
        }

        #endregion

        ICollision CalculateCollision(IBarrier barrier, DynamicBody body, TimeSpan start, TimeSpan end, int depth) {
            var startState = CalculateState(body, start);
            var endState = CalculateState(body, end);

            if (barrier.IsCollision(body, startState, endState)) {
                if (++depth == MaxCollisionDepth)
                    return new Collision(start, barrier, body);

                var middle = TimeSpan.FromSeconds((start + end).TotalSeconds/2.0);
                var firstHalfCollision = CalculateCollision(barrier, body, start, middle, depth);

                if (firstHalfCollision != null)
                    return firstHalfCollision;

                var secondHalfCollision = CalculateCollision(barrier, body, middle, end, depth);

                return secondHalfCollision;
            }
            return null;
        }

        void UpdateKinematics(TimeSpan elapsed) {
            foreach (var physicsObject in _physicsObjects) {
                physicsObject.Body.State = CalculateState(physicsObject.Body, elapsed);
            }
        }

        BodyState CalculateState(DynamicBody body, TimeSpan elapsed) {
            var position = body.Position + body.Velocity*elapsed.TotalSeconds;
            var velocityResult = CalculateVelocity(body, elapsed);

            return new BodyState(
                position,
                velocityResult.Velocity,
                velocityResult.IsInRest
                );
        }

        VelocityResult CalculateVelocity
            (DynamicBody body, TimeSpan elapsed) {
            var impulse = body.Forces.Aggregate(Vector2.Zero,
                                                (current, force) =>
                                                current + force.CalculateForce(body))
                          *elapsed.TotalSeconds; //TODO: requires mass;

            var velocityBeforeResistance = body.Velocity + impulse;

            var resistiveImpulse = body.ResistiveForces.Aggregate(Vector2.Zero,
                                                                  (c, f) =>
                                                                  c +
                                                                  f.CalculateForce(body))
                                   *elapsed.TotalSeconds;

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
            var dynamicBody = new DynamicBody();
            message.GameObject.Body = dynamicBody;
            _physicsObjects.Add(new PhysicsObject(message.GameObject, dynamicBody));
            _eventTriggerer.Trigger(new GameObjectAdded(message.GameObject));
        }

        void ChangePosition
            (PositionChangeRequest
                 message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.Body.State = new BodyState(message.Position, Vector2.Zero, false);
            _eventTriggerer.Trigger(new PositionChanged(message.GameObject));
        }

        void ApplyImpulse
            (ApplyImpulseRequest
                 message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            //TODO: requires mass;
            physicsObject.Body.State = new BodyState(physicsObject.Body.Position,
                                                     physicsObject.Body.Velocity + message.Impulse, false);
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

    public class Collision : ICollision
    {
        public Collision(TimeSpan collisionTime, IBarrier barrier, DynamicBody body) {
            CollisionTime = collisionTime;
            Barrier = barrier;
            Body = body;
        }

        #region ICollision Members

        public TimeSpan CollisionTime { get; private set; }
        public IBarrier Barrier { get; private set; }
        public DynamicBody Body { get; private set; }

        public void Apply(IEventTriggerer eventTriggerer) {
            throw new NotImplementedException();
        }

        #endregion
    }
}