using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics
{
    public class BodyState
    {
        public BodyState(Vector2 position, Vector2 velocity, bool isInRest) {
            Position = position;
            Velocity = velocity;
            IsInRest = isInRest;
        }

        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public bool IsInRest { get; private set; }
    }
}