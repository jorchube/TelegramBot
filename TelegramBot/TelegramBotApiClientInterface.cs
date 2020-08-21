using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public interface TelegramBotApiClientInterface
    {
        public void GetUpdates();

        public void SendMessage();
    }
}
