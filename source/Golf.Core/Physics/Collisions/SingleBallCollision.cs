using System;
using Golf.Core.Events;
using Golf.Core.GameObjects;
using Golf.Core.Maths;

namespace Golf.Core.Physics.Collisions
{
    public class SingleBallCollision : ICollision
    {
        readonly GameObjectBase _gameObject;
        readonly Vector2 _impulse;

        public SingleBallCollision(TimeSpan collisionTime,
                                   GameObjectBase gameObject,
                                   Vector2 impulse) {
            CollisionTime = collisionTime;
            _gameObject = gameObject;
            _impulse = impulse;
        }

        #region ICollision Members

        public TimeSpan CollisionTime { get; private set; }

        public void Apply(IEventTriggerer eventTriggerer) {
            eventTriggerer.Trigger(new ApplyImpulseRequest(_gameObject, _impulse));
        }

        #endregion
    }
}