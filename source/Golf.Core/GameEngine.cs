using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Golf.Core.Events;
using Golf.Core.GameObjects;
using Golf.Core.Maths;
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
            PlayersBall = new GolfBall {
                                           Mass = 1.0,
                                           Friction = 150.0
                                       };

            EventManager.Add(new GameObjectCreated<GolfBall>(PlayersBall));
            EventManager.Add(new ChangePosition(PlayersBall, new Vector2(100, 100)));
        }

        public void PlayShot(double powerX, double powerY) {
            EventManager.Add(new ApplyImpulse(PlayersBall, new Vector2(powerX, powerY)));

            Task.Factory.StartNew(RunShotToCompletion).ContinueWith(t => EventManager.Add(new ShotComplete()));
        }

        void RunShotToCompletion() {
            while(PlayersBall.Body.Velocity.Length > 0.000001) {
                _physicsEngine.Tick(TimeSpan.FromMilliseconds(10));

                Thread.Sleep(10);
            }
        }

        #endregion
    }

    public class ShotComplete : IGameEvent
    {}
}