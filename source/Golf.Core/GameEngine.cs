using System;
using Golf.Core.Events;
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

        public void Start() {
            _physicsEngine.Start();
            
            EventManager.Add(new GameObjectCreated());

            EventManager.TriggerAll();
        }

        #endregion
    }
}