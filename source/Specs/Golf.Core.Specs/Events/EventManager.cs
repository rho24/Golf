using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Golf.Core.Events;

namespace Golf.Core.Specs.Events
{
    public class EventManager : IEventManager
    {
        readonly Subject<IGameEvent> _events;

        public EventManager() {
            WaitingEvents = new List<IGameEvent>();
            _events = new Subject<IGameEvent>();
        }

        public ICollection<IGameEvent> WaitingEvents { get; private set; }

        #region IEventManager Members

        public void Add(IGameEvent gameEvent) {
            WaitingEvents.Add(gameEvent);
        }

        public IObservable<IGameEvent> Events {
            get { return _events; }
        }

        public void TriggerAll() {
            var toTrigger = WaitingEvents;
            WaitingEvents = new List<IGameEvent>();

            foreach (var gameEvent in toTrigger) {
                _events.OnNext(gameEvent);
            }
        }

        #endregion
    }
}