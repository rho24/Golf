using Golf.Core.GameObjects;
using Golf.Core.Maths;

namespace Golf.Core.Events
{
    public class ApplyImpulse : IGameEvent
    {
        public GameObjectBase GameObject { get; private set; }
        public Vector2 Impulse { get; private set; }

        public ApplyImpulse(GameObjectBase gameObject, Vector2 impulse) {
            GameObject = gameObject;
            Impulse = impulse;
        }
    }
}