using System;

namespace Golf.Core.Events
{
    public interface IGameObjectCreated<out T> : IGameEvent
    {
        T GameObject { get; }
    }

    public class GameObjectCreated<T> : IGameObjectCreated<T>
    {
        public T GameObject { get; private set; }

        public GameObjectCreated(T gameObject) {
            GameObject = gameObject;
        }
    }
}