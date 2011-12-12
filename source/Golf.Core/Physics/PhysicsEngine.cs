using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;

namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        readonly IEventTriggerer _eventTriggerer;

        readonly ICollection<PhysicsObject> _physicsObjects = new List<PhysicsObject>();

        public PhysicsEngine(IObservable<IGameEvent> events, IEventTriggerer eventTriggerer) {
            _eventTriggerer = eventTriggerer;

            events
                .OfType<IAddGameObjectRequest>()
                .Subscribe(AddGameObject);


            events
                .OfType<PositionChangeRequest>()
                .Subscribe(ChangePosition);

            events
                .OfType<ApplyImpulse>()
                .Subscribe(ApplyImpulse);
        }

        #region IPhysicsEngine Members

        public void Tick(TimeSpan tickPeriod) {
            foreach (var physicsObject in _physicsObjects) {
                physicsObject.DynamicBody.Position +=
                    physicsObject.DynamicBody.Velocity*tickPeriod.TotalSeconds;

                var frictionInpulse = (-physicsObject.DynamicBody.Velocity.Normal)
                                      *
                                      Math.Min(physicsObject.GameObject.Friction*tickPeriod.TotalSeconds,
                                               physicsObject.DynamicBody.Velocity.Length);

                physicsObject.DynamicBody.Velocity += frictionInpulse;
            }
            _eventTriggerer.Trigger(new ShouldRender());
        }

        #endregion

        void AddGameObject(IAddGameObjectRequest message) {
            var dynamicBody = new DynamicBody();
            message.GameObject.Body = dynamicBody;
            _physicsObjects.Add(new PhysicsObject(message.GameObject, dynamicBody));
            _eventTriggerer.Trigger(new ShouldRender());
        }

        void ChangePosition(PositionChangeRequest message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Position = message.Position;
            _eventTriggerer.Trigger(new PositionChanged(physicsObject.GameObject));
            _eventTriggerer.Trigger(new ShouldRender());
        }

        void ApplyImpulse(ApplyImpulse message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Velocity += message.Impulse;
            _eventTriggerer.Trigger(new ShouldRender());
        }
    }
}