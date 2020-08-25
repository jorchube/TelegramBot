using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class TelegramBotOutgoingMessage
    {
        public TelegramBotOutgoingMessage(long chat_id, string text)
        {
            this.chat_id = chat_id;
            this.text = text;
        }

        public long chat_id { get; }
        public string text { get; }
    }
}
