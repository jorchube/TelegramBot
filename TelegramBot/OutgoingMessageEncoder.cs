using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace TelegramBotApp
{
    public static class OutgoingMessageEncoder
    {
        public static string Encode(OutgoingMessage message)
        {
            return JsonSerializer.Serialize(message);
        }
    }
}
