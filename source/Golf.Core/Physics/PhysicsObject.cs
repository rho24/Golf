using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Physics
{
    public class PhysicsObject
    {
        public GameObjectBase GameObject { get; private set; }
        public DynamicBody DynamicBody { get; private set; }

        public PhysicsObject(GameObjectBase gameObject, DynamicBody dynamicBody) {
            GameObject = gameObject;
            DynamicBody = dynamicBody;
        }
    }
}