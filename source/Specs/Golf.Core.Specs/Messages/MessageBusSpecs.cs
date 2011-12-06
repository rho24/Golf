using System;
using FakeItEasy;
using Golf.Core.Messages;
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
                static readonly DummyMessage _message = new DummyMessage();
                Because of = () => sut.Publish(_message);

                It should_succeed = () => true.ShouldBeTrue();
            }


            public class when_the_subscriber_is_subscribed
            {
                static ISubscriber<DummyMessage> _subscriber;

                Establish context = () => { _subscriber = fake.an<ISubscriber<DummyMessage>>(); };
                Because of = () => sut.Subscribe(_subscriber);

                It should_no_longer_be_a_subscriber = () => sut.Subscribers.ShouldContain(_subscriber);
            }
        }

        public class when_there_is_subscriber
        {
            static ISubscriber<DummyMessage> _subscriber;

            Establish context = () => {
                                    _subscriber = fake.an<ISubscriber<DummyMessage>>();
                                    sut_setup.run(m => m.Subscribe(_subscriber));
                                };

            public class when_a_message_is_published
            {
                static readonly DummyMessage _message = new DummyMessage();
                Because of = () => sut.Publish(_message);

                It should_pass_it_onto_the_subscriber =
                    () => A.CallTo(() => _subscriber.Receive(_message)).MustHaveHappened();
            }

            public class when_the_subscriber_is_unsubscribed
            {
                Because of = () => sut.UnSubscribe(_subscriber);

                It should_no_longer_be_a_subscriber = () => sut.Subscribers.ShouldNotContain(_subscriber);
            }
        }

        public class when_there_is_subscriber_of_a_subclass
        {
            static ISubscriber<SubDummyMessage> _subscriber;

            Establish context = () => {
                                    _subscriber = fake.an<ISubscriber<SubDummyMessage>>();
                                    sut_setup.run(m => m.Subscribe(_subscriber));
                                };

            public class when_a_message_is_published
            {
                static readonly SubDummyMessage _message = new SubDummyMessage();
                Because of = () => sut.Publish(_message);

                It should_pass_it_onto_the_subscriber =
                    () => A.CallTo(() => _subscriber.Receive(_message)).MustHaveHappened();
            }
        }
    }

    public class SubDummyMessage : DummyMessage
    {}

    public class DummyMessage : IMessage
    {}
}