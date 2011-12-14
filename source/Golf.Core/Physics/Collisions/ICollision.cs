using System;
using Golf.Core.Physics.Barriers;

namespace Golf.Core.Physics.Collisions
{
    public interface ICollision
    {
        TimeSpan CollisionTime { get; }
        IBarrier Barrier { get; }
        DynamicBody Body { get; }
        void Apply(IEventTriggerer eventTriggerer);
    }
}