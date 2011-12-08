using System;
using System.Collections.Generic;
using System.Linq;

namespace Golf.Core.Events
{
    public class MessageBus : IMessageBus
    {
        readonly ICollection<ISubscriber> _subscribers;

        public MessageBus() {
            _subscribers = new List<ISubscriber>();
        }

        public IEnumerable<ISubscriber> Subscribers {
            get { return _subscribers; }
        }

        public void Subscribe(ISubscriber subscriber) {
            _subscribers.Add(subscriber);
        }

        public void Publish<T>(T message) where T : IGameEvent {
            foreach (var subscriber in _subscribers.OfType<ISubscriber<T>>()) {
                subscriber.Receive(message);
            }
        }

        public void UnSubscribe(ISubscriber subscriber) {
            _subscribers.Remove(subscriber);
        }
    }
}