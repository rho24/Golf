using System;
using Golf.Core.Physics.BoundingBoxes;

namespace Golf.Core.Physics.Surfaces
{
    public class RectangleSurface : ISurface
    {
        public RectangleSurface(RectangleBoundingBox boundingBox, ConstantResistiveForce frictionForce) {
            BoundingBox = boundingBox;
            FrictionForce = frictionForce;
        }

        public RectangleBoundingBox BoundingBox { get; private set; }

        public double Friction { get; set; }

        #region ISurface Members

        IBoundingBox ISurface.BoundingBox {
            get { return BoundingBox; }
        }

        public ConstantResistiveForce FrictionForce { get; private set; }

        #endregion
    }
}