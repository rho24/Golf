using System;

namespace Golf.Core.Events
{
    public interface ISubscriber
    {}

    public interface ISubscriber<in T> : ISubscriber where T : IGameEvent
    {
        void Receive(T message);
    }
}