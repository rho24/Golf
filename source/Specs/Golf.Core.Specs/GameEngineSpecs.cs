using System;
using FakeItEasy;
using Golf.Core.Physics;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs
{
    [Subject(typeof (GameEngine))]
    public class GameEngineSpecs : Observes<GameEngine>
    {
        static IPhysicsEngine physicsEngine;

        Establish context = () => { physicsEngine = depends.on<IPhysicsEngine>(); };

        public class when_it_is_started
        {

            Because of = () => sut.Start();

            It should_start_the_PhysicsEngine = () => A.CallTo(() => physicsEngine.Start()).MustHaveHappened();
        }
    }

    public class GameEngine : IGameEngine
    {
        readonly IPhysicsEngine _physicsEngine;
        public GameEngine(IPhysicsEngine physicsEngine) {
            _physicsEngine = physicsEngine;
        }

        public void Start() {
            _physicsEngine.Start();
        }
    }
}