using Golf.Core.Physics.Barriers;

namespace Golf.Core.Events
{
    public class AddBarrierRequest : IGameEvent
    {
        public IBarrier Barrier { get; private set; }

        public AddBarrierRequest(IBarrier barrier) {
            Barrier = barrier;
        }
    }
}