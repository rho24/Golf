using System;
using System.Collections.Generic;
using Golf.Core.Maths;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Physics
{
    public class DynamicBody : IBody
    {
        public DynamicBody() {
            Forces = new List<IForce>();
            ResistiveForces = new List<IForce>();
        }

        public ICollection<IForce> Forces { get; set; }
        public ICollection<IForce> ResistiveForces { get; set; }

        public bool IsInRest {
            get { return State.IsInRest; }
        }

        public BodyState State { get; set; }

        #region IBody Members

        public Vector2 Position {
            get { return State.Position; }
        }

        public Vector2 Velocity {
            get { return State.Velocity; }
        }

        #endregion
    }
}