using System;
using System.Collections.Generic;
using Golf.Core.Maths;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics
{
    public class DynamicBody : IDynamicBody
    {
        public DynamicBody() {
            Position = Vector2.Zero;
            Velocity = Vector2.Zero;
            Forces = new List<IForce>();
            ResistiveForces = new List<IForce>();
        }

        public ICollection<IForce> Forces { get; set; }
        public ICollection<IForce> ResistiveForces { get; set; }
        public bool IsInRest { get; set; }

        #region IDynamicBody Members

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        #endregion
    }
}