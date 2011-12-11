using System;
using Golf.Core.Physics;

namespace Golf.Core.GameObjects
{
    public abstract class GameObjectBase
    {
        IDynamicBody _body;
        public IDynamicBody Body {
            get { return _body; }
            set { _body = value; }
        }

        public double Mass { get; set; }
        public double Friction { get; set; }
    }
}