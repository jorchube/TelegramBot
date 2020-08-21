using NUnit.Framework;
using System.Threading.Tasks;
using TelegramBotApp;
using TelegramBotTests.stubs;

namespace TelegramBotTests
{
    public class Tests
    {
        string API_TOKEN = "apitoken";
        HttpClientStub http_client_stub;

        [SetUp]
        public void Setup()
        {
            http_client_stub = new HttpClientStub();
        }

        [TearDown]
        public void Teardown()
        {
            http_client_stub.Dispose();
        }

        [Test]
        public void BotReceivesASaluteAndAnswersTheUser()
        {
            string hello_message = @"{
                'ok': true,
                'result': [
                    {
                        'update_id': 153480413,
                        'message': {
                            'message_id': 2,
                            'from': {
                                'id': 344365009,
                                'is_bot': false,
                                'first_name': 'John',
                                'last_name': 'Doe',
                                'language_code': 'en'
                            },
                            'chat': {
                                'id': 344365009,
                                'first_name': 'John',
                                'last_name': 'Doe',
                                'type': 'private'
                            },
                            'date': 1597997582,
                            'text': 'Hello'
                        }
                    }
                ]
            }";

            TelegramBotApiClient api_client = new TelegramBotApiClient(API_TOKEN, http_client_stub);
            TelegramBot bot = new TelegramBot(api_client);

            StartBot(bot);

            InjectUpdate(hello_message);

            StopBot(bot);

            Assert.AreEqual("Hello John", SentMessage());
        }

        async void StartBot(TelegramBot bot)
        {
            await Task.Run(() => bot.Start());
        }

        void StopBot(TelegramBot bot)
        {
            bot.Stop();
        }

        void InjectUpdate(string get_update_response)
        {
            http_client_stub.InjectGetResponse(get_update_response);
        }

        string SentMessage()
        {
            return http_client_stub.GetSentMessageText();
        }
    }
}