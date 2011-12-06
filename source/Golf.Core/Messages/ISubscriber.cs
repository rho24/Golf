using System;

namespace Golf.Core.Messages
{
    public interface ISubscriber
    {}

    public interface ISubscriber<in T> : ISubscriber where T : IMessage
    {
        void Receive(T message);
    }
}