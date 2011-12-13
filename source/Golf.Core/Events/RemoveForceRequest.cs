using System;
using Golf.Core.GameObjects;
using Golf.Core.Physics.Forces;

namespace Golf.Core.Events
{
    public class RemoveForceRequest : IGameEvent
    {
        public RemoveForceRequest(GameObjectBase gameObject, IForce force) {
            GameObject = gameObject;
            Force = force;
        }

        public GameObjectBase GameObject { get; private set; }
        public IForce Force { get; private set; }
    }
}