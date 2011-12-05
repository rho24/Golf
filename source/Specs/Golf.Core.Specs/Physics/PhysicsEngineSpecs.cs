using System;
using Golf.Core.Physics;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs.Physics
{
    [Subject(typeof (PhysicsEngine))]
    public class PhysicsEngineSpecs : Observes<PhysicsEngine>
    {
        public class when_it_is_started
        {
            Because of = () => sut.Start();

            It should_say_its_running = () => sut.Running.ShouldBeTrue();
        }
    }
}