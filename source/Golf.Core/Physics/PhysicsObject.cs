using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Physics
{
    public class PhysicsObject
    {
        public GameObjectBase GameObject { get; private set; }
        public DynamicBody Body { get; private set; }

        public PhysicsObject(GameObjectBase gameObject, DynamicBody body) {
            GameObject = gameObject;
            Body = body;
        }
    }
}