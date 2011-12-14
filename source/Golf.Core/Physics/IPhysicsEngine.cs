using System;

namespace Golf.Core.Physics
{
    public interface IPhysicsEngine
    {
        bool IsInRest { get; }
        void Tick(TickTime tickTime);
    }
}