using Golf.Core.Maths;

namespace Golf.Core.Physics
{
    public interface IBody
    {
        Vector2 Position { get; }
        Vector2 Velocity { get; }
    }
}