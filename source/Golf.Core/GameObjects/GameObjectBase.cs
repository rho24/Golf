using System;
using Golf.Core.Physics;
using Golf.Core.Physics.Surfaces;

namespace Golf.Core.GameObjects
{
    public abstract class GameObjectBase
    {
        public IDynamicBody Body { get; set; }
        public double Mass { get; set; }
        public Surface Surface { get; set; }
    }
}