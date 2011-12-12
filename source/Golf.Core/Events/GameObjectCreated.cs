using System;
using Golf.Core.GameObjects;

namespace Golf.Core.Events
{
    public interface IAddGameObjectRequest : IGameEvent
    {
        GameObjectBase GameObject { get; }
    }

    public class AddGameObjectRequest<T> : IAddGameObjectRequest where T : GameObjectBase
    {
        public AddGameObjectRequest(T gameObject) {
            GameObject = gameObject;
        }

        public T GameObject { get; private set; }

        #region IAddGameObjectRequest Members

        GameObjectBase IAddGameObjectRequest.GameObject {
            get { return GameObject; }
        }

        #endregion
    }
}