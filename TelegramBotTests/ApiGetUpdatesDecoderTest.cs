using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class ApiGetUpdatesDecoderTest
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithNoUpdates()
        {
            List<UpdateMessage> updates = ApiGetUpdatesDecoder.Decode(GET_UPDATES_EMPTY_RESPONSE);

            Assert.IsEmpty(updates);
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithOneUpdate()
        {
            List<UpdateMessage> updates = ApiGetUpdatesDecoder.Decode(GET_UPDATES_ONE_UPDATE_RESPONSE);

            Assert.AreEqual(1, updates.Count);
            Assert.AreEqual(EXPECTED_ONE_UPDATE, updates.First());
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithManyUpdates()
        {
            List<UpdateMessage> updates = ApiGetUpdatesDecoder.Decode(GET_UPDATES_MANY_UPDATES_RESPONSES);

            Assert.AreEqual(2, updates.Count);
            Assert.IsTrue(EXPECTED_MANY_UPDATES.All(update => updates.Contains(update)));
        }

        string GET_UPDATES_EMPTY_RESPONSE = "{\"ok\": true,\"result\": []}";

        string GET_UPDATES_ONE_UPDATE_RESPONSE = @"{
            ""ok"": true,
            ""result"": [
                {
                    ""update_id"": 153480413,
                    ""message"": {
                        ""message_id"": 2,
                        ""from"": {
                            ""id"": 344365009,
                            ""is_bot"": false,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""language_code"": ""en""
                        },
                        ""chat"": {
                            ""id"": 344365009,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""type"": ""private""
                        },
                        ""date"": 1597997582,
                        ""text"": ""sample text""
                    }
                }
            ]
        }";
        UpdateMessage EXPECTED_ONE_UPDATE = new UpdateMessage(
            id: 153480413,
            message: new UpdateMessage.Message(
                id: 2,
                date: 1597997582,
                text: "sample text",
                from: new UpdateMessage.User(
                    id: 344365009,
                    first_name: "John",
                    last_name: "Doe"
                ),
                chat: new UpdateMessage.Chat(id: 344365009)
            )
        );

        string GET_UPDATES_MANY_UPDATES_RESPONSES = @"{
            ""ok"": true,
            ""result"": [
                {
                    ""update_id"": 153480413,
                    ""message"": {
                        ""message_id"": 2,
                        ""from"": {
                            ""id"": 344365009,
                            ""is_bot"": false,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""language_code"": ""en""
                        },
                        ""chat"": {
                            ""id"": 344365009,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""type"": ""private""
                        },
                        ""date"": 1597997582,
                        ""text"": ""sample text""
                    }
                },
                {
                    ""update_id"": 153480414,
                    ""message"": {
                        ""message_id"": 3,
                        ""from"": {
                            ""id"": 344365009,
                            ""is_bot"": false,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""language_code"": ""en""
                        },
                        ""chat"": {
                            ""id"": 344365009,
                            ""first_name"": ""John"",
                            ""last_name"": ""Doe"",
                            ""type"": ""private""
                        },
                        ""date"": 1597997586,
                        ""text"": ""another sample text""
                    }
                }
            ]
        }";
        List<UpdateMessage> EXPECTED_MANY_UPDATES = new List<UpdateMessage> {
            new UpdateMessage(
                id: 153480413,
                message: new UpdateMessage.Message(
                    id: 2,
                    date: 1597997582,
                    text: "sample text",
                    from: new UpdateMessage.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe"
                    ),
                    chat: new UpdateMessage.Chat(id: 344365009)
                )
            ),
            new UpdateMessage(
                id: 153480414,
                message: new UpdateMessage.Message(
                    id: 3,
                    date: 1597997586,
                    text: "another sample text",
                    from: new UpdateMessage.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe"
                    ),
                    chat: new UpdateMessage.Chat(id: 344365009)
                )
            )
        };
    }
}
