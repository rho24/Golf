using System;
using Golf.Core.Maths;
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

        #region IResistiveForce Members

        public Vector2 CalculateForce(DynamicBody body) {
            return -body.Velocity.Normal*_friction;
        }

        #endregion
    }
}