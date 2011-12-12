using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Events
{
    public class GameObjectAdded : IGameEvent
    {
        public GameObjectAdded(GameObjectBase gameObject) {
            GameObject = gameObject;
        }

        public GameObjectBase GameObject { get; private set; }
    }
}