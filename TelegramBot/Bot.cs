using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    public class Bot : BotInterface
    {
        BotRunnerInterface runner;
        ApiClientInteface api_client;
        List<UpdateHandlerInterface> update_handlers;
        LoggerInterface logger;

        public Bot(ApiClientInteface api_client, BotRunnerInterface runner, LoggerInterface logger = null)
        {
            this.runner = runner;
            this.api_client = api_client;
            this.update_handlers = new List<UpdateHandlerInterface>();
            this.logger = logger ?? new NullLogger();
        }

        public void Start()
        {
            runner.Start(this);

            this.logger.Log("Started");
        }

        public void Stop()
        {
            runner.Stop();

            this.logger.Log("Stopped");
        }

        public List<UpdateMessage> GetUpdates()
        {
            logger.Log("Waiting for updates...");

            return api_client.GetUpdates();
        }

        public void InstallUpdateHandler(UpdateHandlerInterface handler)
        {
            update_handlers.Add(handler);
        }

        public void HandleUpdate(UpdateMessage update)
        {
            logger.Log($"Handling update from {update.message.from.first_name} {update.message.from.last_name} in {update.message.chat.type} chat -> \"{update.message.text}\"");

            foreach (UpdateHandlerInterface handler in update_handlers) {
                Task.Run(() => handler.HandleUpdate(update, HandleUpdateCallback));
            }
        }

        public void HandleUpdateCallback(OutgoingMessage response_message)
        {
            api_client.SendMessage(response_message);
        }


        class NullLogger : LoggerInterface
        {
            public void Log(string message_format, params object[] args)
            {
                return;
            }
        }
    }
}
