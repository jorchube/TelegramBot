using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class Bot : BotInterface
    {
        BotRunnerInterface runner;
        ApiClientInteface api_client;

        public Bot(ApiClientInteface api_client, BotRunnerInterface runner)
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

        public List<UpdateMessage> GetUpdates()
        {
            return api_client.GetUpdates();
        }

        public void HandleUpdate(UpdateMessage update)
        {
            OutgoingMessage response = new OutgoingMessage(
                chat_id: update.message.chat.id,
                text: "Hello " + update.message.from.first_name
            );

            api_client.SendMessage(response);
        }
    }
}
