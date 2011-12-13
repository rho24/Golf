using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics
{
    public class VelocityResult
    {
        public Vector2 Velocity { get; private set; }
        public bool IsInRest { get; private set; }

        public VelocityResult(Vector2 velocity, bool isInRest) {
            Velocity = velocity;
            IsInRest = isInRest;
        }
    }
}