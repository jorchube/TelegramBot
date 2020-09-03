using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    public interface HttpClientWrapperInterface
    {
        public enum HttpStatusCodeWrapper {
            OK = HttpStatusCode.OK,
            BadRequest = HttpStatusCode.BadRequest,
        }

        public class HttpResponseMessageWrapper
        {
            public HttpStatusCodeWrapper StatusCode { get; }
            public string Content { get; }

            public HttpResponseMessageWrapper(HttpResponseMessage http_response_mesage)
            {
                StatusCode = http_response_mesage.StatusCode == HttpStatusCode.OK ? HttpStatusCodeWrapper.OK : HttpStatusCodeWrapper.BadRequest;
                Content = Task.Run(async () => await http_response_mesage.Content.ReadAsStringAsync()).Result;
            }
        }



        public HttpResponseMessageWrapper Get(string requestUri);
        public void Post(string requestUri, string content);

        //public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}
