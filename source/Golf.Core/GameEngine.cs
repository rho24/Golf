using System;
using Golf.Core.Events;
using Golf.Core.Physics;

namespace Golf.Core
{
    public class GameEngine : IGameEngine
    {
        readonly IPhysicsEngine _physicsEngine;

        public GameEngine(IPhysicsEngine physicsEngine, IMessageBus messageBus) {
            _physicsEngine = physicsEngine;
            MessageBus = messageBus;
        }

        #region IGameEngine Members

        public IMessageBus MessageBus { get; private set; }

        public IObservable<IGameEvent> Events { get; private set; }

        public void Start() {
            _physicsEngine.Start();

            MessageBus.Publish(new GameObjectCreated());
        }

        #endregion
    }
}