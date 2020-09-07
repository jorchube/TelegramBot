using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TelegramBotApp.UpdateHandlers
{
    public class PoliteBotHandler : UpdateHandlerInterface
    {
        List<string> matchers = new List<string> {
            @"^\s*hello+(\s+bot)?\!*\s*$",
            @"^\s*hi+(\s+bot)?\!*\s*$",
            @"^\s*good morning\!*\s*$",
            @"^\s*good evening\!*\s*$",
            @"^\s*greetings(\s+bot)?\!*\s*$",
        };

        public void HandleUpdate(UpdateMessage message, UpdateHandlerInterface.HandleUpdateCallback response_callback)
        {
            string text = message.message.text;
            long chat_id = message.message.chat.id;

            if (ShallAnswer(text)) {
                response_callback(new OutgoingMessage(chat_id, "Hi!!!"));
            }
        }

        bool ShallAnswer(string text)
        {
            return matchers.Any(matcher => Regex.IsMatch(text, matcher, RegexOptions.IgnoreCase));
        }
    }
}
