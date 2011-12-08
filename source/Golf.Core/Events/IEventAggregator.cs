using System;

namespace Golf.Core.Events
{
    public interface IEventAggregator
    {
         void Add(IGameEvent gameEvent);
    }
}