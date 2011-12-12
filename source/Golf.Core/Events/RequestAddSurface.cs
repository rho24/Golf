using Golf.Core.Physics.Surfaces;

namespace Golf.Core.Events
{
    public class RequestAddSurface : IGameEvent
    {
        public ISurface Surface { get; private set; }

        public RequestAddSurface(ISurface surface) {
            Surface = surface;
        }
    }
}