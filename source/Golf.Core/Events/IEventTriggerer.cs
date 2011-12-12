using System;

namespace Golf.Core.Events
{
    public interface IEventTriggerer
    {
        void Trigger(IGameEvent gameEvent);
    }
}