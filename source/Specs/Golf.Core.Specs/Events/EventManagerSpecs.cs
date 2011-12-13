using System;
using FakeItEasy;
using Golf.Core.Events;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs.Events
{
    [Subject(typeof (EventManager))]
    public class EventManagerSpecs : Observes<EventManager>
    {}

    public class when_Add_is_called : EventManagerSpecs
    {
        static IObserver<IGameEvent> _observer;
        static IGameEvent _event;

        Establish context = () => {
                                _observer = A.Fake<IObserver<IGameEvent>>();
                                _event = A.Fake<IGameEvent>();
                                sut_setup.run(sut => sut.Events.Subscribe(_observer));
                            };

        Because of = () =>
                     sut.Trigger(_event);

        It should_trigger_the_event = () => A.CallTo(() => _observer.OnNext(_event)).MustHaveHappened();
    }
}