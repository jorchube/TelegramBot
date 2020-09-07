using System;
using System.Threading;
using System.Threading.Tasks;
using TelegramBotApp.UpdateHandlers;

namespace TelegramBotApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string api_token = args[0];

            HttpClientWrapper http_client = new HttpClientWrapper();
            ApiClient api_client = new ApiClient(api_token, http_client);
            BotRunner runner = new BotRunner();

            Bot bot = new Bot(api_client, runner);

            InstallUpdateHandlersToBot(bot);

            bot.Start();

            while (true) {
                Thread.Sleep(30);
            }
        }

        static void InstallUpdateHandlersToBot(Bot bot)
        {
            bot.InstallUpdateHandler(new PoliteBotHandler());
        }
    }
}
