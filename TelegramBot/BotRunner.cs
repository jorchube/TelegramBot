using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    public class BotRunner : BotRunnerInterface
    {
        CancellationTokenSource cancellation_token_source;

        public BotRunner()
        {
            cancellation_token_source = new CancellationTokenSource();
        }

        public void Start(BotInterface bot)
        {
            Task.Run(() => {
                while (true) {
                    HandleBotUpdates(bot);
                }
            },
            cancellation_token_source.Token);
        }

        public void Stop()
        {
            cancellation_token_source.Cancel();
        }

        private void HandleBotUpdates(BotInterface bot)
        {
            List<UpdateMessage> updates = bot.GetUpdates();

            foreach (UpdateMessage update in updates) {
                bot.HandleUpdate(update);
            }
        }
    }
}
