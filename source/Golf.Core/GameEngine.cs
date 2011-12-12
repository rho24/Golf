using System;
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
        readonly IEventTriggerer _eventTriggerer;
        readonly IPhysicsEngine _physicsEngine;

        public GameEngine(IPhysicsEngine physicsEngine, IEventTriggerer eventTriggerer) {
            _physicsEngine = physicsEngine;
            _eventTriggerer = eventTriggerer;
        }

        #region IGameEngine Members

        public GolfBall PlayersBall { get; private set; }

        public void Initialize() {
            PlayersBall = new GolfBall {
                                           Mass = 1.0,
                                           Friction = 150.0
                                       };

            _eventTriggerer.Trigger(new AddGameObjectRequest<GolfBall>(PlayersBall));
            _eventTriggerer.Trigger(new PositionChangeRequest(PlayersBall, new Vector2(100, 100)));
        }

        public void PlayShot(double powerX, double powerY) {
            _eventTriggerer.Trigger(new ApplyImpulse(PlayersBall, new Vector2(powerX, powerY)));

            Task.Factory.StartNew(RunShotToCompletion).ContinueWith(t => _eventTriggerer.Trigger(new ShotComplete()));
        }

        #endregion

        void RunShotToCompletion() {
            while (PlayersBall.Body.Velocity.Length > 0.000001) {
                _physicsEngine.Tick(TimeSpan.FromMilliseconds(10));

                Thread.Sleep(10);
            }
        }
    }
}