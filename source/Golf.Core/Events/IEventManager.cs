using System;

namespace Golf.Core.Events
{
    public interface IEventManager:IEventTriggerer
    {
        IObservable<IGameEvent> Events { get; }
        void TriggerAll();
    }
}