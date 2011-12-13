using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using Golf.Core.Events;

namespace Golf.Core.Physics.Barriers
{
    public class BarrierManager : IBarriers
    {
        readonly ICollection<IBarrier> _barriers;

        public BarrierManager(IObservable<IGameEvent> events ) {
            
            _barriers = new List<IBarrier>();

            events.OfType<AddBarrierRequest>().Subscribe(AddBarrier);
        }

        void AddBarrier(AddBarrierRequest e) {
            _barriers.Add(e.Barrier);
        }

        public IEnumerator<IBarrier> GetEnumerator() {
            return _barriers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}