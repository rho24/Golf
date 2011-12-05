using System;
using Golf.Core.Physics;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs.Physics
{
    [Subject(typeof (PhysicsEngine))]
    public class PhysicsEngineSpecs : Observes<PhysicsEngine>
    {
        #region Nested type: when_it_is_started

        public class when_it_is_started
        {
            private Because of = () => sut.Start();

            private It should_say_its_running = () => sut.Running.ShouldBeTrue();
        }

        #endregion
    }
}