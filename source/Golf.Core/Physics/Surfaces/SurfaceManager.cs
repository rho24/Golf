using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;

namespace Golf.Core.Physics.Surfaces
{
    public class SurfaceManager : ISurfaceManager
    {
        readonly IEventTriggerer _eventTriggerer;
        readonly ICollection<ISurface> _surfaces = new List<ISurface>();

        public SurfaceManager(IObservable<IGameEvent> events, IEventTriggerer eventTriggerer) {
            _eventTriggerer = eventTriggerer;
            events.OfType<RequestAddSurface>().Subscribe(AddSurface);
            events.OfType<PositionChanged>().Subscribe(PositionChanged);
        }

        void AddSurface(RequestAddSurface e) {
            _surfaces.Add(e.Surface);
            _eventTriggerer.Trigger(new SurfaceAdded(e.Surface));
        }

        void PositionChanged(PositionChanged e) {
            var surface = _surfaces.Where(s => s.BoundingBox.Contains(e.GameObject.Body.Position)).Single();

            e.GameObject.Surface = surface;
        }
    }
}