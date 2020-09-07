using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests.UpdateHandlers
{
    class PoliteBotHandlerTests
    {
        int handle_update_callback_num_calls;

        [SetUp]
        public void Setup()
        {
            handle_update_callback_num_calls = 0;
        }

        [Test]
        public void ItDoesNotAnswerNonGreetingMessages()
        {
            TelegramBotApp.UpdateHandlers.PoliteBotHandler handler = new TelegramBotApp.UpdateHandlers.PoliteBotHandler();

            HandleUpdate(handler, "lalala");
            HandleUpdate(handler, "you touch my tralala");
            HandleUpdate(handler, "tell me");
            HandleUpdate(handler, "why");
            HandleUpdate(handler, "never");
            HandleUpdate(handler, "gonna give you up");
            HandleUpdate(handler, "1234");
            HandleUpdate(handler, "$#@$%()*(@#");

            Assert.AreEqual(0, handle_update_callback_num_calls);
        }

        [Test]
        public void ItAnswersGreetingMessages()
        {
            TelegramBotApp.UpdateHandlers.PoliteBotHandler handler = new TelegramBotApp.UpdateHandlers.PoliteBotHandler();

            HandleUpdate(handler, "Hello");
            HandleUpdate(handler, "hello bot");
            HandleUpdate(handler, "hi");
            HandleUpdate(handler, "hi!!!");
            HandleUpdate(handler, "Good morning");
            HandleUpdate(handler, "good evEnIng");
            HandleUpdate(handler, "greetings bot!!!");
            HandleUpdate(handler, "hiiiii!!");
            HandleUpdate(handler, "hellooooo bot!!");

            Assert.AreEqual(9, handle_update_callback_num_calls);
        }

        void HandleUpdateCallback(OutgoingMessage response_message)
        {
            handle_update_callback_num_calls++;
        }

        void HandleUpdate(UpdateHandlerInterface handler, string message_text)
        {
            handler.HandleUpdate(MessageWithText(message_text), HandleUpdateCallback);
        }

        UpdateMessage MessageWithText(string text)
        {
            return new UpdateMessage(
                id: 153480413,
                message: new UpdateMessage.Message(
                    id: 2,
                    date: 1597997582,
                    text: text,
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
    }
}
