using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApp
{
    public class TelegramBotOutgoingMessage
    {
        public TelegramBotOutgoingMessage()
        {
        }

        public TelegramBotOutgoingMessage(long chat_id, string text)
        {
            this.chat_id = chat_id;
            this.text = text;
        }

        public long chat_id { get; set; }
        public string text { get; set; }

        public override bool Equals(object other)
        {
            if (other == null) {
                return false;
            }

            return this.chat_id == (other as TelegramBotOutgoingMessage).chat_id && this.text == (other as TelegramBotOutgoingMessage).text;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(chat_id, text);
        }
    }
}
