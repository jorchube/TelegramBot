using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;

namespace TelegramBotTests.stubs
{
    public class HttpClientStub : HttpClient
    {
        public void InjectGetResponse(string response)
        {
        }

        public string GetSentMessageText()
        {
            return "";
        }
    }
}
