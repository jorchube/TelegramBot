using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class LoggerTest
    {
        Logger logger;
        string logged_message;

        [SetUp]
        public void Setup()
        {
            logger = new Logger("TelegramBot", LoggerCallback, DateTimeCallback);
            logged_message = null;
        }

        [Test]
        public void ItLogsAMessage()
        {
            logger.Log("a message");

            Assert.AreEqual("[2019-07-21 16:55:59.700] TelegramBot: a message", logged_message);

            logger.Log("a {0} message than {1}", "longer", "before");

            Assert.AreEqual("[2019-07-21 16:55:59.700] TelegramBot: a longer message than before", logged_message);
        }

        void LoggerCallback(string message)
        {
            logged_message = message;
        }

        DateTime DateTimeCallback()
        {
            return new DateTime(2019, 07, 21, 16, 55, 59, 700);
        }
    }
}
