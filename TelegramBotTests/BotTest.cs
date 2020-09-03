using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class BotTest
    {
        Mock<ApiClientInteface> api_client_mock;
        Mock<BotRunnerInterface> bot_runner_mock;

        [SetUp]
        public void Setup()
        {
            api_client_mock = new Mock<ApiClientInteface>();
            bot_runner_mock = new Mock<BotRunnerInterface>();
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
        public void BotHandlesASaluteMessageUpdate()
        {
            OutgoingMessage expected_message = new OutgoingMessage(chat_id: 344365009, text: "Hello John");
            UpdateMessage received_update = new UpdateMessage(
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

            Bot bot = new Bot(api_client_mock.Object, bot_runner_mock.Object);

            bot.HandleUpdate(received_update);

            api_client_mock.Verify(api_client => api_client.SendMessage(expected_message));
        }
    }
}
