using System;
using System.Collections.Generic;

namespace Golf.Core.Events
{
    public interface IMessageBus
    {
        IEnumerable<ISubscriber> Subscribers { get; }
        void Subscribe(ISubscriber subscriber);
        void Publish<T>(T message) where T : IGameEvent;
        void UnSubscribe(ISubscriber subscriber);
    }
}