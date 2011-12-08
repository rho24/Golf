using System;

namespace Golf.Core.Events
{
    public interface IEventManager:IEventAggregator
    {
        IObservable<IGameEvent> Events { get; }
        void TriggerAll();
    }
}