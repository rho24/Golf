using Golf.Core.GameObjects;
using Golf.Core.Maths;

namespace Golf.Core.Physics
{
    public class DynamicBody:IDynamicBody
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public DynamicBody() {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
        }
    }
}