using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;
using System.Threading;
using TelegramBotApp;
using static TelegramBotApp.HttpClientWrapperInterface;

namespace TelegramBotTests.stubs
{
    public class HttpClientWrapperStub : HttpClientWrapperInterface
    {
        string get_response;
        string get_request_uri;

        string post_request_uri;
        string post_request_content;

        public HttpClientWrapperStub()
        {
        }

        public void InjectGetResponse(string response)
        {
            get_response = response;
        }

        public string GetRequestUri()
        {
            return get_request_uri;
        }

        public string PostRequestUri()
        {
            return post_request_uri;
        }

        public string PostRequestContent()
        {
            return post_request_content;
        }

        public HttpResponseMessageWrapper Get(string request_uri)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpContent content = new StringContent(get_response);

            get_request_uri = request_uri;

            response.StatusCode = HttpStatusCode.OK;
            response.Content = content;

            return new HttpResponseMessageWrapper(response);
        }

        public void Post(string request_uri, string content)
        {
            post_request_uri = request_uri;
            post_request_content = content;
        }
    }
}
