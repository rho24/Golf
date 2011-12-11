using System;
using Golf.Core.Physics;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs.Physics
{
    [Subject(typeof (PhysicsEngine))]
    public class PhysicsEngineSpecs : Observes<PhysicsEngine>
    {
    }
}