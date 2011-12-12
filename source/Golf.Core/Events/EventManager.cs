using System;
using System.Reactive.Subjects;

namespace Golf.Core.Events
{
    public class EventManager : IEventManager
    {
        readonly Subject<IGameEvent> _events;

        public EventManager() {
            _events = new Subject<IGameEvent>();
        }

        #region IEventManager Members

        public void Add(IGameEvent gameEvent) {
            _events.OnNext(gameEvent);
        }

        public IObservable<IGameEvent> Events {
            get { return _events; }
        }

        #endregion
    }
}