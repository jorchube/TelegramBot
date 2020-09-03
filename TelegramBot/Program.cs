using System;
using System.Threading;
using System.Threading.Tasks;

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

            bot.Start();

            while (true) {
                // FIXME: change this to stdin read to orderly terminate the program upon key press
                Thread.Sleep(30);
            }
        }
    }
}
