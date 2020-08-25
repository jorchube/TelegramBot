using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface TelegramBotApiClientInteface
    {
        public List<TelegramBotUpdate> GetUpdates();
        public void SendMessage(TelegramBotOutgoingMessage message);
    }
}
