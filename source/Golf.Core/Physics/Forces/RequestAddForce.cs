using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Core.Physics.Forces
{
    public class RequestAddForce : IGameEvent
    {
        public GameObjectBase GameObject { get; private set; }
        public IForce Force { get; private set; }

        public RequestAddForce(GameObjectBase gameObject, IForce force) {
            GameObject = gameObject;
            Force = force;
        }
    }
}