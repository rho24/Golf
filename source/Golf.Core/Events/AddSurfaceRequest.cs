using Golf.Core.Physics.Surfaces;

namespace Golf.Core.Events
{
    public class AddSurfaceRequest : IGameEvent
    {
        public ISurface Surface { get; private set; }

        public AddSurfaceRequest(ISurface surface) {
            Surface = surface;
        }
    }
}