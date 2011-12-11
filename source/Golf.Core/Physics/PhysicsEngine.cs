using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;

namespace Golf.Core.Physics
{
    public class PhysicsEngine : IPhysicsEngine
    {
        readonly IEventManager _eventManager;

        readonly ICollection<PhysicsObject> _physicsObjects = new List<PhysicsObject>();

        public PhysicsEngine(IEventManager eventManager) {
            _eventManager = eventManager;

            _eventManager.Events
                .OfType<IGameObjectCreated>()
                .Subscribe(GameObjectCreated);


            _eventManager.Events
                .OfType<ChangePosition>()
                .Subscribe(ChangePosition);

            _eventManager.Events
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
                                      Math.Min(physicsObject.GameObject.Friction * tickPeriod.TotalSeconds,
                                               physicsObject.DynamicBody.Velocity.Length);

                physicsObject.DynamicBody.Velocity += frictionInpulse;
            }
            _eventManager.Add(new ShouldRender());
            _eventManager.TriggerAll();
        }

        #endregion

        void GameObjectCreated(IGameObjectCreated message) {
            var dynamicBody = new DynamicBody();
            message.GameObject.Body = dynamicBody;
            _physicsObjects.Add(new PhysicsObject(message.GameObject, dynamicBody));
            _eventManager.Add(new ShouldRender());
            _eventManager.TriggerAll();
        }

        void ChangePosition(ChangePosition message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Position = message.Position;
            _eventManager.Add(new ShouldRender());
            _eventManager.TriggerAll();
        }

        void ApplyImpulse(ApplyImpulse message) {
            var physicsObject = _physicsObjects.Where(p => p.GameObject == message.GameObject).Single();

            physicsObject.DynamicBody.Velocity += message.Impulse;
            _eventManager.Add(new ShouldRender());
            _eventManager.TriggerAll();
        }
    }
}