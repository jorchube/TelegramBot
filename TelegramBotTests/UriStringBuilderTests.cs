using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class UriStringBuilderTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ItCreatesUriString()
        {
            UriStringBuilder builder = new UriStringBuilder("token", "an_endpoint");

            Assert.AreEqual("https://api.telegram.org/bottoken/an_endpoint", builder.Build());
        }

        [Test]
        public void ItCreatesUriStringWithOneQueryParameter()
        {
            UriStringBuilder builder = new UriStringBuilder("token", "an_endpoint");
            builder.WithQueryParameter("param", "value");

            Assert.AreEqual("https://api.telegram.org/bottoken/an_endpoint?param=value", builder.Build());
        }

        [Test]
        public void ItCreatesUriStringWithManyQueryParameters()
        {
            UriStringBuilder builder = new UriStringBuilder("token", "an_endpoint");
            builder.WithQueryParameter("param1", "value1");
            builder.WithQueryParameter("param2", "value2");
            builder.WithQueryParameter("param3", "value3");

            Assert.AreEqual("https://api.telegram.org/bottoken/an_endpoint?param1=value1&param2=value2&param3=value3", builder.Build());
        }
    }
}
