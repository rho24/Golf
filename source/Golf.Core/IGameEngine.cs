using System;
using Golf.Core.Messages;

namespace Golf.Core
{
    public interface IGameEngine
    {
        IMessageBus MessageBus { get; }
        void Start();
    }
}