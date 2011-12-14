using System;
using System.Threading;
using System.Threading.Tasks;
using Golf.Core.Events;
using Golf.Core.GameObjects;
using Golf.Core.Maths;
using Golf.Core.Physics;
using Golf.Core.Physics.Barriers;
using Golf.Core.Physics.BoundingBoxes;
using Golf.Core.Physics.Surfaces;

namespace Golf.Core
{
    public class GameEngine : IGameEngine
    {
        readonly IEventTriggerer _eventTriggerer;
        readonly IPhysicsEngine _physicsEngine;
        readonly ISurfaceManager _surfaceManager;

        public GameEngine(IPhysicsEngine physicsEngine, ISurfaceManager surfaceManager, IEventTriggerer eventTriggerer) {
            _physicsEngine = physicsEngine;
            _surfaceManager = surfaceManager;
            _eventTriggerer = eventTriggerer;
        }

        #region IGameEngine Members

        public GolfBall PlayersBall { get; private set; }

        public void Initialize() {
            _eventTriggerer.Trigger(new AddSurfaceRequest(
                                        new RectangleSurface(
                                            new RectangleBoundingBox(
                                                new Vector2(20, 20),
                                                new Vector2(800, 700)),
                                            new ConstantResistiveForce(150))));

            _eventTriggerer.Trigger(new AddBarrierRequest(new LineBarrier(
                new Vector2(400, 40),
                new Vector2(400, 680))));

            PlayersBall = new GolfBall {
                                           Mass = 1.0
                                       };

            _eventTriggerer.Trigger(new AddGameObjectRequest(PlayersBall, new Vector2(100, 100)));
            //_eventTriggerer.Trigger(new AddForceRequest(PlayersBall, new MagnetForce(new Vector2(400, 350), 100000)));
        }

        public void PlayShot(double powerX, double powerY) {
            _eventTriggerer.Trigger(new ApplyImpulseRequest(PlayersBall, new Vector2(powerX, powerY)));

            Task.Factory.StartNew(RunShotToCompletion).ContinueWith(t => _eventTriggerer.Trigger(new ShotComplete()));
        }

        #endregion

        void RunShotToCompletion() {
            var totalElapsed = TimeSpan.Zero;
            do {
                var tickElapsed = TimeSpan.FromMilliseconds(10);
                var tickTime = new TickTime(tickElapsed, totalElapsed);
                _physicsEngine.Tick(tickTime);
                totalElapsed += tickElapsed;
                Thread.Sleep(tickElapsed.Milliseconds);
            } while (!_physicsEngine.IsInRest);
        }
    }

    public class TickTime
    {
        public TimeSpan TickElapsed { get; private set; }
        public TimeSpan TotalElapsed { get; private set; }

        public TickTime(TimeSpan tickElapsed, TimeSpan totalElapsed) {
            TickElapsed = tickElapsed;
            TotalElapsed = totalElapsed;
        }
    }
}