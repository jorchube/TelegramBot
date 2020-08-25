using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class TelegramBot : TelegramBotInterface
    {
        TelegramBotRunnerInterface runner;
        TelegramBotApiClientInteface api_client;

        public TelegramBot(TelegramBotApiClientInteface api_client, TelegramBotRunnerInterface runner)
        {
            this.runner = runner;
            this.api_client = api_client;
        }

        public void Start()
        {
            runner.Start(this);
        }

        public void Stop()
        {
            runner.Stop();
        }

        public List<TelegramBotUpdate> GetUpdates()
        {
            return api_client.GetUpdates();
        }

        public void HandleUpdate(TelegramBotUpdate update)
        {
        
        }
    }
}
