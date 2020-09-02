using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TelegramBotApp
{
    public static class TelegramBotApiGetUpdatesDecoder
    {
        class GetUpdatesContent
        {
            [JsonPropertyName("ok")]
            public bool Success { get; set; }

            [JsonPropertyName("result")]
            public List<TelegramBotUpdate> Updates { get; set; }
        }

        public static List<TelegramBotUpdate> Decode(string get_updates_content)
        {
            GetUpdatesContent content = JsonSerializer.Deserialize<GetUpdatesContent>(get_updates_content);

            return content.Updates;
        }
    }
}
