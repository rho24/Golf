namespace Golf.Core.Physics.Surfaces
{
    public class Surface
    {
        public IBoundingBox BoundingBox { get; private set; }
        public double Friction { get; private set; }

        public Surface(IBoundingBox boundingBox, double friction) {
            BoundingBox = boundingBox;
            Friction = friction;
        }
    }
}