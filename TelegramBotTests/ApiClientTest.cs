using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TelegramBotApp;
using TelegramBotTests.stubs;

namespace TelegramBotTests
{
    class ApiClientTest
    {
        string TEST_API_TOKEN = "api_token";
        HttpClientWrapperStub http_client_stub;

        ApiClient api_client;

        [SetUp]
        public void Setup()
        {
            http_client_stub = new HttpClientWrapperStub();

            api_client = new ApiClient(TEST_API_TOKEN, http_client_stub);
        }

        [Test]
        public void IsCreatesANewApiClient()
        {
            Assert.IsNotNull(api_client);
        }

        [Test]
        public void ItSendsGetUpdatesRequestAndReceivesUpdatesListWithNoUpdates()
        {
            List<UpdateMessage> updates;
            string expected_get_request_uri = "https://api.telegram.org/botapi_token/getupdates?timeout=60&offset=0";
            http_client_stub.InjectGetResponse(GET_UPDATES_EMPTY_RESPONSE);

            updates = api_client.GetUpdates();

            Assert.IsEmpty(updates);
            Assert.AreEqual(expected_get_request_uri, http_client_stub.GetRequestUri());
        }

        [Test]
        public void ItSendsGetUpdatesRequestAndReceivesUpdatesListWithUpdates()
        {
            List<UpdateMessage> updates;
            string expected_get_request_uri = "https://api.telegram.org/botapi_token/getupdates?timeout=60&offset=0";
            http_client_stub.InjectGetResponse(GET_UPDATES_ONE_RESPONSE);
        
            updates = api_client.GetUpdates();

            Assert.AreEqual(1, updates.Count);
            Assert.AreEqual(expected_get_request_uri, http_client_stub.GetRequestUri());
        }

        [Test]
        public void ItUpdatesGetUpdatesRequestOffsetAccordingToLastUpdatesReceived()
        {
            List<UpdateMessage> updates;
            long expected_offset;
            string expected_get_request_uri_offset_fmt = "https://api.telegram.org/botapi_token/getupdates?timeout=60&offset={0}";
            
            http_client_stub.InjectGetResponse(GET_UPDATES_MANY_UPDATES_RESPONSES);

            updates = api_client.GetUpdates();
            expected_offset = updates.Last().update_id + 1;
            _ = api_client.GetUpdates();

            Assert.AreEqual(String.Format(expected_get_request_uri_offset_fmt, expected_offset), http_client_stub.GetRequestUri());
        }

        [Test]
        public void ItSendsAMessage()
        {
            OutgoingMessage message = new OutgoingMessage(chat_id: 1234, text: "yay");
            string expected_post_request_uri = "https://api.telegram.org/botapi_token/sendmessage";

            api_client.SendMessage(message);

            Assert.AreEqual(expected_post_request_uri, http_client_stub.PostRequestUri());
            Assert.AreEqual(@"{""chat_id"":1234,""text"":""yay""}", http_client_stub.PostRequestContent());
        }

        string GET_UPDATES_EMPTY_RESPONSE = "{\"ok\": true,\"result\": []}";
        string GET_UPDATES_ONE_RESPONSE = @"{
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
    }
}
