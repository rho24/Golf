using System;
using Golf.Core.Messages;
using Golf.Core.Physics;

namespace Golf.Core
{
    public class GameEngine : IGameEngine
    {
        readonly IPhysicsEngine _physicsEngine;

        public GameEngine(IPhysicsEngine physicsEngine) {
            _physicsEngine = physicsEngine;
        }

        #region IGameEngine Members

        public IMessageBus MessageBus { get; private set; }

        public void Start() {
            _physicsEngine.Start();
        }

        #endregion
    }
}