using System;
using Golf.Core.Events;

namespace Golf.Core
{
    public interface IGameEngine
    {
        IMessageBus MessageBus { get; }
        IObservable<IGameEvent> Events { get; }
        void Start();
    }
}