using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface ApiClientInteface
    {
        public List<UpdateMessage> GetUpdates();
        public void SendMessage(OutgoingMessage message);
    }
}
