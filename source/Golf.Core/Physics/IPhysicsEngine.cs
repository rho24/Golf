using System;
using System.Collections.Generic;
using Golf.Core.GameObjects;

namespace Golf.Core.Physics
{
    public interface IPhysicsEngine
    {
        void Tick(TimeSpan tickPeriod);
    }
}