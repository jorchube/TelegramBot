using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface BotInterface
    {
        public void Start();
        public void Stop();
        public List<UpdateMessage> GetUpdates();
        public void HandleUpdate(UpdateMessage update);
    }
}
