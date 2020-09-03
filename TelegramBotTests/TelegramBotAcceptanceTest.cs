using NUnit.Framework;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotApp;
using TelegramBotTests.stubs;

namespace TelegramBotAcceptanceTests
{
    public class TelegramBotAcceptanceTests
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
        public void BotReceivesASaluteAndAnswersTheUser()
        {
            string hello_message = @"{
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

            TelegramBotApiClient api_client = new TelegramBotApiClient(API_TOKEN, http_client_stub);
            TelegramBotRunner bot_runner = new TelegramBotRunner();
            TelegramBot bot = new TelegramBot(api_client, bot_runner);

            bot.Start();

            InjectUpdate(hello_message);

            Thread.Sleep(millisecondsTimeout: 200);
            bot.Stop();

            Assert.AreEqual("Hello John", SentMessage().text);
            Assert.AreEqual(344365009, SentMessage().chat_id);
        }

        void InjectUpdate(string get_update_response)
        {
            http_client_stub.InjectGetResponse(get_update_response);
        }

        TelegramBotOutgoingMessage SentMessage()
        {
            string content = http_client_stub.PostRequestContent();

            return JsonSerializer.Deserialize<TelegramBotOutgoingMessage>(content);
        }
    }
}