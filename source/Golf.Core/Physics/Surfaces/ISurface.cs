using System;
using Golf.Core.Maths;
using Golf.Core.Physics.BoundingBoxes;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics.Surfaces
{
    public interface ISurface
    {
        IBoundingBox BoundingBox { get; }
        ConstantResistiveForce FrictionForce { get; }
    }

    public class ConstantResistiveForce : IResistiveForce
    {
        readonly double _friction;

        public ConstantResistiveForce(double friction) {
            _friction = friction;
        }

        #region IForce Members

        public Vector2 CalculateImpulse(DynamicBody body, TimeSpan tickPeriod) {
            return (-body.Velocity.Normal)
                   *
                   _friction*tickPeriod.TotalSeconds;
        }

        #endregion
    }

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