using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using static TelegramBotApp.HttpClientWrapperInterface;

namespace TelegramBotApp
{
    public class HttpClientWrapper : HttpClientWrapperInterface
    {
        HttpClient http_client;

        public HttpClientWrapper()
        {
            http_client = new HttpClient();
        }

        public HttpResponseMessageWrapper Get(string requestUri)
        {
            return null;
        }

        public void Post(string requestUri, string content)
        {
            throw new NotImplementedException();
        }
    }
}
