using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TelegramBotApp
{
    public class TelegramBotApiClient: TelegramBotApiClientInteface
    {
        public TelegramBotApiClient(string api_token, HttpClient http_client)
        {
        }

        public List<TelegramBotUpdate> GetUpdates()
        {
            return new List<TelegramBotUpdate>();
        }
        
        public void SendMessage(TelegramBotOutgoingMessage message)
        {

        }
    }
}
