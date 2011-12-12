using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Golf.Core.Events;

namespace Golf.Core.Physics.Surfaces
{
    public class SurfaceManager : ISurfaceManager
    {
        public SurfaceManager(IObservable<IGameEvent> events) {
            events.OfType<PositionChanged>().Subscribe(PositionChanged);
        }

        #region ISurfaceManager Members

        public IEnumerable<Surface> Surfaces { get; set; }

        #endregion

        void PositionChanged(PositionChanged e) {
            var surface = Surfaces.Where(s => s.BoundingBox.Contains(e.GameObject.Body.Position)).Single();

            e.GameObject.Surface = surface;
        }
    }
}