using System;
using Golf.Core.GameObjects;
using Golf.Core.Maths;

namespace Golf.Core.Events
{
    public class AddGameObjectRequest : IGameEvent
    {
        public AddGameObjectRequest(GameObjectBase gameObject, Vector2 position) {
            GameObject = gameObject;
            Position = position;
        }

        public GameObjectBase GameObject { get; private set; }
        public Vector2 Position { get; private set; }
    }
}