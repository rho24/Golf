using System;
using Golf.Core.Maths;

namespace Golf.Core.Physics.Forces
{
    public class LinearForce : IForce
    {
        readonly Vector2 _force;

        public LinearForce(Vector2 force) {
            _force = force;
        }

        #region IForce Members

        public Vector2 CalculateForce(DynamicBody body) {
            return _force;
        }

        #endregion
    }
}