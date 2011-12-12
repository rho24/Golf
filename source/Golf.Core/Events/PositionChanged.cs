using Golf.Core.GameObjects;

namespace Golf.Core.Events
{
    public class PositionChanged : IGameEvent
    {
        public GameObjectBase GameObject { get; private set; }

        public PositionChanged(GameObjectBase gameObject) {
            GameObject = gameObject;
        }
    }
}