using System;
using Golf.Core.Events;

namespace Golf.Core.Physics
{
    public interface ISurfaceManager
    {
         
    }

    public class SurfaceManager : ISurfaceManager
    {
        public SurfaceManager(IObservable<IGameEvent> events) {
        }
    }
}