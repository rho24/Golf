using Golf.Core.Physics.Surfaces;

namespace Golf.Core.Events
{
    public class SurfaceAdded : IGameEvent
    {
        public ISurface Surface { get; private set; }

        public SurfaceAdded(ISurface surface) {
            Surface = surface;
        }
    }
}