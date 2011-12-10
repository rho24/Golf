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

    }
}