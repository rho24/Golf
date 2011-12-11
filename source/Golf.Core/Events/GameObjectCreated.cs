using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Events
{
    public interface IGameObjectCreated : IGameEvent
    {
        GameObjectBase GameObject { get; }
    }

    public class GameObjectCreated<T> : IGameObjectCreated where T : GameObjectBase
    {
        public GameObjectCreated(T gameObject) {
            GameObject = gameObject;
        }

        public T GameObject { get; private set; }

        #region IGameObjectCreated Members

        GameObjectBase IGameObjectCreated.GameObject {
            get { return GameObject; }
        }

        #endregion
    }
}