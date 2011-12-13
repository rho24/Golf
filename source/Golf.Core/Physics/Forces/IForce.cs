using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics.Forces
{
    public interface IForce
    {
        Vector2 CalculateImpulse(DynamicBody body, TimeSpan tickPeriod);
    }
}