using System;
using Golf.Core.Physics.BoundingBoxes;

namespace Golf.Core.Physics.Surfaces
{
    public interface ISurface
    {
        IBoundingBox BoundingBox { get; }
        double Friction { get; }
    }

    public class RectangleSurface : ISurface
    {
        public RectangleBoundingBox BoundingBox { get; private set; }

        #region ISurface Members

        IBoundingBox ISurface.BoundingBox {
            get { return BoundingBox; }
        }

        public RectangleSurface(RectangleBoundingBox boundingBox, double friction) {
            BoundingBox = boundingBox;
            Friction = friction;
        }

        public double Friction { get; set; }

        #endregion
    }
}