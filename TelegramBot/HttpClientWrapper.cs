using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
            HttpResponseMessage response = Task.Run(async () => await http_client.GetAsync(requestUri)).Result;

            return new HttpResponseMessageWrapper(response);
        }

        public void Post(string requestUri, string content)
        {
            HttpContent http_content = new StringContent(content);
            http_content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Task.Run(async () => await http_client.PostAsync(requestUri, http_content));
        }
    }
}
