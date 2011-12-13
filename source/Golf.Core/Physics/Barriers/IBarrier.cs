using System;
using Golf.Core.GameObjects;
using Golf.Core.Physics.Collisions;

namespace Golf.Core.Physics.Barriers
{
    public interface IBarrier
    {
        ICollision CalculateCollision(GameObjectBase gameObject, TimeSpan tickPeriod);
    }
}