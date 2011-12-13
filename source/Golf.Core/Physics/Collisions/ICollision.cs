using System;

namespace Golf.Core.Physics.Collisions
{
    public interface ICollision
    {
        TimeSpan CollisionTime { get; }
        void Apply(IEventTriggerer eventTriggerer);
    }
}