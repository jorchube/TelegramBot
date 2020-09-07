using NUnit.Framework;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotApp;
using TelegramBotTests.stubs;

namespace BotAcceptanceTests
{
    public class BotAcceptanceTests
    {
        string API_TOKEN = "apitoken";
        HttpClientWrapperStub http_client_stub;

        [SetUp]
        public void Setup()
        {
            http_client_stub = new HttpClientWrapperStub();
        }

        [TearDown]
        public void Teardown()
        {
        }

        [Test]
        public void BotHandlesAnUpdateMessage()
        {
            ApiClient api_client = new ApiClient(API_TOKEN, http_client_stub);
            BotRunner bot_runner = new BotRunner();
            Bot bot = new Bot(api_client, bot_runner);
            bot.InstallUpdateHandler(new HelloTestHandler());

            bot.Start();

            InjectUpdate(TEST_HELLO_MESSAGE);

            Thread.Sleep(millisecondsTimeout: 200);
            bot.Stop();

            Assert.AreEqual("Hello John", SentMessage().text);
            Assert.AreEqual(344365009, SentMessage().chat_id);
        }

        void InjectUpdate(string get_update_response)
        {
            http_client_stub.InjectGetResponse(get_update_response);
        }

        OutgoingMessage SentMessage()
        {
            string content = http_client_stub.PostRequestContent();

            return JsonSerializer.Deserialize<OutgoingMessage>(content);
        }

        class HelloTestHandler : UpdateHandlerInterface
        {
            public void HandleUpdate(UpdateMessage message, UpdateHandlerInterface.HandleUpdateCallback response_callback)
            {
                OutgoingMessage response = new OutgoingMessage(message.message.chat.id, "Hello " + message.message.from.first_name);

                response_callback(response);
            }
        }

        const string TEST_HELLO_MESSAGE = @"{
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
                        ""text"": ""Hello""
                    }
                }
            ]
        }";
    }
}