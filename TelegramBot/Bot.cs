using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegramBotApp
{
    public class Bot : BotInterface
    {
        BotRunnerInterface runner;
        ApiClientInteface api_client;
        List<UpdateHandlerInterface> update_handlers;

        public Bot(ApiClientInteface api_client, BotRunnerInterface runner)
        {
            this.runner = runner;
            this.api_client = api_client;
            this.update_handlers = new List<UpdateHandlerInterface>();
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

        public void InstallUpdateHandler(UpdateHandlerInterface handler)
        {
            update_handlers.Add(handler);
        }

        public void HandleUpdate(UpdateMessage update)
        {
            foreach (UpdateHandlerInterface handler in update_handlers) {
                handler.HandleUpdate(update, HandleUpdateCallback);
            }
        }

        public void HandleUpdateCallback(OutgoingMessage response_message)
        {
            api_client.SendMessage(response_message);
        }
    }
}
