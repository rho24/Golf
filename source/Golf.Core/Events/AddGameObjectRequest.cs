using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Events
{
    public class AddGameObjectRequest : IGameEvent
    {
        public AddGameObjectRequest(GameObjectBase gameObject) {
            GameObject = gameObject;
        }

        public GameObjectBase GameObject { get; private set; }
    }
}