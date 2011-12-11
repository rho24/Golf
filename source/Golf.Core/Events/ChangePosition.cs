using Golf.Core.GameObjects;
using Golf.Core.Maths;

namespace Golf.Core.Events
{
    public class ChangePosition : IGameEvent
    {
        public ChangePosition(GameObjectBase gameObject, Vector2 position) {
            GameObject = gameObject;
            Position = position;
        }

        public GameObjectBase GameObject { get; private set; }
        public Vector2 Position { get; private set; }
    }
}