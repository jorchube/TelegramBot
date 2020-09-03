using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    public class TelegramBotRunner : TelegramBotRunnerInterface
    {
        CancellationTokenSource cancellation_token_source;

        public TelegramBotRunner()
        {
            cancellation_token_source = new CancellationTokenSource();
        }

        public void Start(TelegramBotInterface bot)
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

        private void HandleBotUpdates(TelegramBotInterface bot)
        {
            List<TelegramBotUpdate> updates = bot.GetUpdates();

            foreach (TelegramBotUpdate update in updates) {
                bot.HandleUpdate(update);
            }
        }
    }
}
