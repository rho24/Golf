using System;
using System.Reactive.Subjects;

namespace Golf.Core
{
    public class EventManager : IEventTriggerer
    {
        readonly Subject<IGameEvent> _events;

        public EventManager() {
            _events = new Subject<IGameEvent>();
        }

        public IObservable<IGameEvent> Events {
            get { return _events; }
        }

        #region IEventTriggerer Members

        public void Trigger(IGameEvent gameEvent) {
            _events.OnNext(gameEvent);
        }

        #endregion
    }
}