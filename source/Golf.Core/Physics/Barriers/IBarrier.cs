using System;

namespace Golf.Core.Physics.Barriers
{
    public interface IBarrier
    {
        bool IsCollision(DynamicBody body, BodyState startState, BodyState endState);
        void ApplyCollision(IEventTriggerer eventTriggerer, DynamicBody body);
    }
}