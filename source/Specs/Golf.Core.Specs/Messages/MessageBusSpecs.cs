using System;
using FakeItEasy;
using Golf.Core.Events;
using Machine.Specifications;
using developwithpassion.specifications.fakeiteasy;

namespace Golf.Core.Specs.Messages
{
    [Subject(typeof (MessageBus))]
    public class MessageBusSpecs : Observes<MessageBus>
    {
        public class when_there_is_no_subscriber
        {
            public class when_a_message_is_published
            {
                static readonly DummyGameEvent GameEvent = new DummyGameEvent();
                Because of = () => sut.Publish(GameEvent);

                It should_succeed = () => true.ShouldBeTrue();
            }


            public class when_the_subscriber_is_subscribed
            {
                static ISubscriber<DummyGameEvent> _subscriber;

                Establish context = () => { _subscriber = fake.an<ISubscriber<DummyGameEvent>>(); };
                Because of = () => sut.Subscribe(_subscriber);

                It should_no_longer_be_a_subscriber = () => sut.Subscribers.ShouldContain(_subscriber);
            }
        }

        public class when_there_is_subscriber
        {
            static ISubscriber<DummyGameEvent> _subscriber;

            Establish context = () => {
                                    _subscriber = fake.an<ISubscriber<DummyGameEvent>>();
                                    sut_setup.run(m => m.Subscribe(_subscriber));
                                };

            public class when_a_message_is_published
            {
                static readonly DummyGameEvent GameEvent = new DummyGameEvent();
                Because of = () => sut.Publish(GameEvent);

                It should_pass_it_onto_the_subscriber =
                    () => A.CallTo(() => _subscriber.Receive(GameEvent)).MustHaveHappened();
            }

            public class when_the_subscriber_is_unsubscribed
            {
                Because of = () => sut.UnSubscribe(_subscriber);

                It should_no_longer_be_a_subscriber = () => sut.Subscribers.ShouldNotContain(_subscriber);
            }
        }

        public class when_there_is_subscriber_of_a_subclass
        {
            static ISubscriber<SubDummyGameEvent> _subscriber;

            Establish context = () => {
                                    _subscriber = fake.an<ISubscriber<SubDummyGameEvent>>();
                                    sut_setup.run(m => m.Subscribe(_subscriber));
                                };

            public class when_a_message_is_published
            {
                static readonly SubDummyGameEvent GameEvent = new SubDummyGameEvent();
                Because of = () => sut.Publish(GameEvent);

                It should_pass_it_onto_the_subscriber =
                    () => A.CallTo(() => _subscriber.Receive(GameEvent)).MustHaveHappened();
            }
        }
    }

    public class SubDummyGameEvent : DummyGameEvent
    {}

    public class DummyGameEvent : IGameEvent
    {}
}