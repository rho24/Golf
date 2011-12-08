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

    public class when_constructed : EventManagerSpecs
    {
        It should_initialise_WaitingEvents = () => sut.WaitingEvents.ShouldNotBeNull();
    }

    public class when_Add_is_called : EventManagerSpecs
    {
        static IGameEvent _event;

        Establish context = () => _event = fake.an<IGameEvent>();

        Because of = () => sut.Add(_event);

        It should_add_it_to_WaitingEvents = () => sut.WaitingEvents.ShouldContain(_event);
    }

    public class when_TriggerAll_is_called_with_no_waiting_events : EventManagerSpecs
    {
        Because of = () => sut.TriggerAll();

        It should_not_throw = () => true.ShouldBeTrue();
    }

    public class when_TriggerAll_is_called_with_1_waiting_event : EventManagerSpecs
    {
        static IObserver<IGameEvent> _observer;
        static IGameEvent _event;

        Establish context = () => {
                                _observer = A.Fake<IObserver<IGameEvent>>();
                                _event = A.Fake<IGameEvent>();
                                sut_setup.run(sut => {
                                                  sut.Events.Subscribe(_observer);
                                                  sut.WaitingEvents.Add(_event);
                                              });
                            };

        Because of = () => sut.TriggerAll();

        It should_clear_WaitingEvents = () => sut.WaitingEvents.ShouldBeEmpty();
        It should_trigger_the_event = () => A.CallTo(() => _observer.OnNext(_event)).MustHaveHappened();
    }

    public class when_TriggerAll_is_called_with_2_waiting_event : EventManagerSpecs
    {
        static IObserver<IGameEvent> _observer;
        static IGameEvent _event1;
        static IGameEvent _event2;

        Establish context = () => {
                                _observer = A.Fake<IObserver<IGameEvent>>();
                                _event1 = A.Fake<IGameEvent>();
                                _event2 = A.Fake<IGameEvent>();
                                sut_setup.run(sut => {
                                                  sut.Events.Subscribe(_observer);
                                                  sut.WaitingEvents.Add(_event1);
                                                  sut.WaitingEvents.Add(_event2);
                                              });
                            };

        Because of = () => sut.TriggerAll();

        It should_clear_WaitingEvents = () => sut.WaitingEvents.ShouldBeEmpty();
        It should_trigger_event1 = () => A.CallTo(() => _observer.OnNext(_event1)).MustHaveHappened();
        It should_trigger_event2 = () => A.CallTo(() => _observer.OnNext(_event2)).MustHaveHappened();
    }
}