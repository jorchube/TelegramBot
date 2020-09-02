using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class TelegramBotTest
    {
        Mock<TelegramBotApiClientInteface> api_client_mock;
        Mock<TelegramBotRunnerInterface> bot_runner_mock;

        [SetUp]
        public void Setup()
        {
            api_client_mock = new Mock<TelegramBotApiClientInteface>();
            bot_runner_mock = new Mock<TelegramBotRunnerInterface>();
        }

        [Test]
        public void RunnerStartsWithBotWhenStartingBot()
        {
            TelegramBot bot = new TelegramBot(api_client_mock.Object, bot_runner_mock.Object);

            bot.Start();

            bot_runner_mock.Verify(runner => runner.Start(bot));
        }

        [Test]
        public void RunnerStopsWhenStoppingBot()
        {
            TelegramBot bot = new TelegramBot(api_client_mock.Object, bot_runner_mock.Object);

            bot.Stop();

            bot_runner_mock.Verify(runner => runner.Stop());
        }

        [Test]
        public void BotGetsUpdateListFromApiClient()
        {
            List<TelegramBotUpdate> updates;
            List<TelegramBotUpdate> expected_updates = new List<TelegramBotUpdate>();

            api_client_mock.Setup(api_client => api_client.GetUpdates()).Returns(expected_updates);
            
            TelegramBot bot = new TelegramBot(api_client_mock.Object, bot_runner_mock.Object);
            
            updates = bot.GetUpdates();

            Assert.AreSame(expected_updates, updates);
        }

        [Test]
        public void BotHandlesASaluteMessageUpdate()
        {
            TelegramBotOutgoingMessage expected_message = new TelegramBotOutgoingMessage(chat_id: 344365009, text: "Hello John");
            TelegramBotUpdate received_update = new TelegramBotUpdate(
                id: 153480413,
                message: new TelegramBotUpdate.Message(
                    id: 2,
                    date: 1597997582,
                    text: "Hello",
                    from: new TelegramBotUpdate.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe",
                        username: ""
                    ),
                    chat: new TelegramBotUpdate.Chat(id: 344365009)
                )
            );

            TelegramBot bot = new TelegramBot(api_client_mock.Object, bot_runner_mock.Object);

            bot.HandleUpdate(received_update);

            api_client_mock.Verify(api_client => api_client.SendMessage(expected_message));
        }
    }
}
