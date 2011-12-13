using System;
using Golf.Core.Physics;
using Golf.Core.Physics.Surfaces;

namespace Golf.Core.GameObjects
{
    public abstract class GameObjectBase
    {
        public IBody Body { get; set; }
        public double Mass { get; set; }
        public ISurface Surface { get; set; }
    }
}