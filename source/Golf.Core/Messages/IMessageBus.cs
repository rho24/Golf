using System.Collections.Generic;

namespace Golf.Core.Messages
{
    public interface IMessageBus
    {
        IEnumerable<ISubscriber> Subscribers { get; }
        void Subscribe(ISubscriber subscriber);
        void Publish<T>(T message) where T : IMessage;
        void UnSubscribe(ISubscriber subscriber);
    }
}