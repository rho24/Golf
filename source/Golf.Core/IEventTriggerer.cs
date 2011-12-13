using System;

namespace Golf.Core
{
    public interface IEventTriggerer
    {
        void Trigger(IGameEvent gameEvent);
    }
}