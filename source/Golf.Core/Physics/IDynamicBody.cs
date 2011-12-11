using Golf.Core.Maths;

namespace Golf.Core.Physics
{
    public interface IDynamicBody
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; }
    }
}