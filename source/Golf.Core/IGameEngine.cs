using System;
using Golf.Core.Events;
using Golf.Core.GameObjects;

namespace Golf.Core
{
    public interface IGameEngine
    {
        IObservable<IGameEvent> Events { get; }
        GolfBall PlayersBall { get; }
        void Initialize();
        void PlayShot(double powerX, double powerY);
    }
}