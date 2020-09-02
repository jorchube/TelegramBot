﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramBotApp;

namespace TelegramBotTests
{
    class TelegramBotApiGetUpdatesDecoderTest
    {
        [SetUp]
        public void Setup()
        {
        
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithNoUpdates()
        {
            List<TelegramBotUpdate> updates = TelegramBotApiGetUpdatesDecoder.Decode(GET_UPDATES_EMPTY_RESPONSE);

            Assert.IsEmpty(updates);
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithOneUpdate()
        {
            List<TelegramBotUpdate> updates = TelegramBotApiGetUpdatesDecoder.Decode(GET_UPDATES_ONE_UPDATE_RESPONSE);

            Assert.AreEqual(1, updates.Count);
            Assert.AreEqual(EXPECTED_ONE_UPDATE, updates.First());
        }

        [Test]
        public void ItDecodesGetUpdatesAnswerWithManyUpdates()
        {
            List<TelegramBotUpdate> updates = TelegramBotApiGetUpdatesDecoder.Decode(GET_UPDATES_MANY_UPDATES_RESPONSES);

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
        TelegramBotUpdate EXPECTED_ONE_UPDATE = new TelegramBotUpdate(
            id: 153480413,
            message: new TelegramBotUpdate.Message(
                id: 2,
                date: 1597997582,
                text: "sample text",
                from: new TelegramBotUpdate.User(
                    id: 344365009,
                    first_name: "John",
                    last_name: "Doe"
                ),
                chat: new TelegramBotUpdate.Chat(id: 344365009)
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
        List<TelegramBotUpdate> EXPECTED_MANY_UPDATES = new List<TelegramBotUpdate> {
            new TelegramBotUpdate(
                id: 153480413,
                message: new TelegramBotUpdate.Message(
                    id: 2,
                    date: 1597997582,
                    text: "sample text",
                    from: new TelegramBotUpdate.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe"
                    ),
                    chat: new TelegramBotUpdate.Chat(id: 344365009)
                )
            ),
            new TelegramBotUpdate(
                id: 153480414,
                message: new TelegramBotUpdate.Message(
                    id: 3,
                    date: 1597997586,
                    text: "another sample text",
                    from: new TelegramBotUpdate.User(
                        id: 344365009,
                        first_name: "John",
                        last_name: "Doe"
                    ),
                    chat: new TelegramBotUpdate.Chat(id: 344365009)
                )
            )
        };
    }
}