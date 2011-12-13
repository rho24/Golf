using Golf.Core.Maths;

namespace Golf.Core.Physics.Forces
{
    public class MagnetForce : IForce
    {
        readonly Vector2 _centre;
        readonly double _magnitude;

        public MagnetForce(Vector2 centre, double magnitude) {
            _centre = centre;
            _magnitude = magnitude;
        }

        public Vector2 CalculateForce(DynamicBody body) {
            var path = (_centre - body.Position);

            if(path.Length < 10)
                return Vector2.Zero;

            return path.Normal*(_magnitude/10);
        }
    }
}