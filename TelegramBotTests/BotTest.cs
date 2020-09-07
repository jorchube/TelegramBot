using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using TelegramBotApp;

namespace TelegramBotTests
{
    class BotTest
    {
        const int SLOW_TEST_HANDLE_DURATION_MILLIS = 100;

        Mock<ApiClientInteface> api_client_mock;
        Mock<BotRunnerInterface> bot_runner_mock;
        UpdateMessage test_received_update;

        [SetUp]
        public void Setup()
        {
            api_client_mock = new Mock<ApiClientInteface>();
            bot_runner_mock = new Mock<BotRunnerInterface>();
            test_received_update = new UpdateMessage(
                id: 153480413,
                message: new UpdateMessage.Message(
                    id: 2,
                    date: 1597997582,
                    text: "Hello",
                    from: new UpdateMessage.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe",
                        username: ""
                    ),
                    chat: new UpdateMessage.Chat(id: 344365009)
                )
            );
        }

        [Test]
        public void RunnerStartsWithBotWhenStartingBot()
        {
            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.Start();

            bot_runner_mock.Verify(runner => runner.Start(bot));
        }

        [Test]
        public void RunnerStopsWhenStoppingBot()
        {
            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.Stop();

            bot_runner_mock.Verify(runner => runner.Stop());
        }

        [Test]
        public void BotGetsUpdateListFromApiClient()
        {
            List<UpdateMessage> updates;
            List<UpdateMessage> expected_updates = new List<UpdateMessage>();

            api_client_mock.Setup(api_client => api_client.GetUpdates()).Returns(expected_updates);
            
            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);
            
            updates = bot.GetUpdates();

            Assert.AreSame(expected_updates, updates);
        }

        [Test]
        public void BotHandlesAnUpdateMessageWithNoInstalledHandlers()
        {
            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.HandleUpdate(test_received_update);

            api_client_mock.Verify(api_client => api_client.SendMessage(It.IsAny<OutgoingMessage>()), Times.Never);
        }

        [Test]
        public void BotHandlesAnUpdateMessageWithOneInstalledHandler()
        {
            OutgoingMessage expected_message = new OutgoingMessage(chat_id: 344365009, text: "Hello John");
            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.InstallUpdateHandler(new HelloTestHandler());
            bot.HandleUpdate(test_received_update);

            api_client_mock.Verify(api_client => api_client.SendMessage(expected_message));
        }

        [Test]
        public void BotHandlesAnUpdateMessageWithManyInstalledHandlers()
        {
            OutgoingMessage expected_hello_message = new OutgoingMessage(chat_id: 344365009, text: "Hello John");
            OutgoingMessage expected_bye_message = new OutgoingMessage(chat_id: 344365009, text: "John bye!");

            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.InstallUpdateHandler(new HelloTestHandler());
            bot.InstallUpdateHandler(new ByeTestHandler());
            bot.HandleUpdate(test_received_update);

            api_client_mock.Verify(api_client => api_client.SendMessage(expected_hello_message));
            api_client_mock.Verify(api_client => api_client.SendMessage(expected_bye_message));
        }

        [Test]
        public void BotHandlesUpdateMessagesInParallel()
        {
            OutgoingMessage expected_message = new OutgoingMessage(chat_id: 344365009, text: "slow handle");

            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.InstallUpdateHandler(new SlowTestHandler());
            bot.InstallUpdateHandler(new SlowTestHandler());
            bot.InstallUpdateHandler(new SlowTestHandler());
            bot.InstallUpdateHandler(new SlowTestHandler());
            bot.InstallUpdateHandler(new SlowTestHandler());

            bot.HandleUpdate(test_received_update);

            Thread.Sleep(millisecondsTimeout: SLOW_TEST_HANDLE_DURATION_MILLIS * 2);

            api_client_mock.Verify(api_client => api_client.SendMessage(expected_message), Times.Exactly(5));
        }

        class HelloTestHandler : UpdateHandlerInterface
        {
            public void HandleUpdate(UpdateMessage message, UpdateHandlerInterface.HandleUpdateCallback response_callback)
            {
                OutgoingMessage response = new OutgoingMessage(message.message.chat.id, "Hello " + message.message.from.first_name);

                response_callback(response);
            }
        }

        class ByeTestHandler : UpdateHandlerInterface
        {
            public void HandleUpdate(UpdateMessage message, UpdateHandlerInterface.HandleUpdateCallback response_callback)
            {
                OutgoingMessage response = new OutgoingMessage(message.message.chat.id, message.message.from.first_name + " bye!");

                response_callback(response);
            }
        }

        class SlowTestHandler : UpdateHandlerInterface
        {
            public void HandleUpdate(UpdateMessage message, UpdateHandlerInterface.HandleUpdateCallback response_callback)
            {
                OutgoingMessage response = new OutgoingMessage(message.message.chat.id, "slow handle");

                Thread.Sleep(millisecondsTimeout: SLOW_TEST_HANDLE_DURATION_MILLIS);

                response_callback(response);
            }
        }
    }
}
