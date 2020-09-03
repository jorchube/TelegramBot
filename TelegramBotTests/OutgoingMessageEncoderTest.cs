using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class OutgoingMessageEncoderTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ItEncodesAMessage()
        {
            TelegramBotOutgoingMessage message = new TelegramBotOutgoingMessage(chat_id: 33, text: "lalala");

            string encoded = OutgoingMessageEncoder.Encode(message);

            Assert.AreEqual(@"{""chat_id"":33,""text"":""lalala""}", encoded);
        }
    }
}
