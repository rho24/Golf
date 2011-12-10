using System;
using Golf.Core.Events;
using Golf.Core.GameObjects;
using Golf.Core.Physics;

namespace Golf.Core
{
    public class GameEngine : IGameEngine
    {
        readonly IPhysicsEngine _physicsEngine;

        public GameEngine(IPhysicsEngine physicsEngine, IEventManager eventManager) {
            _physicsEngine = physicsEngine;
            EventManager = eventManager;
        }

        public IEventManager EventManager { get; private set; }

        #region IGameEngine Members

        public IObservable<IGameEvent> Events {
            get { return EventManager.Events; }
        }

        public GolfBall PlayersBall { get; private set; }

        public void Initialize() {
            PlayersBall = new GolfBall {X = 100.0, Y = 100.0};
            EventManager.Add(new GameObjectCreated<GolfBall>(PlayersBall));

            EventManager.TriggerAll();
        }

        public void PlayShot(double powerX, double powerY) {
            PlayersBall.X = powerX;
            PlayersBall.Y = powerY;
            EventManager.Add(new TickEvent());
            _physicsEngine.Start();
            EventManager.TriggerAll();
        }

        #endregion
    }
}