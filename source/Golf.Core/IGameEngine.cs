using System;
using Golf.Core.Events;

namespace Golf.Core
{
    public interface IGameEngine
    {
        IObservable<IGameEvent> Events { get; }
        void Start();
    }
}