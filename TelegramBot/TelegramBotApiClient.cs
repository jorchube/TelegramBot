using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static TelegramBotApp.HttpClientWrapperInterface;

namespace TelegramBotApp
{
    public class TelegramBotApiClient: TelegramBotApiClientInteface
    {
        const int REQUEST_TIMEOUT_SECONDS = 60;
        const string GET_UPDATES_ENDPOINT = "getupdates";
        const string SEND_MESSAGE_ENDPOINT = "sendmessage";
        long get_updates_offset;
        string api_token;
        HttpClientWrapperInterface http_client;

        public TelegramBotApiClient(string api_token, HttpClientWrapperInterface http_client)
        {
            this.api_token = api_token;
            this.http_client = http_client;
            this.get_updates_offset = 0;
        }

        public List<TelegramBotUpdate> GetUpdates()
        {
            List<TelegramBotUpdate> updates;
            string uri = GetUpdatesUriString(REQUEST_TIMEOUT_SECONDS, get_updates_offset);

            HttpResponseMessageWrapper response = GetRequest(uri);

            updates = TelegramBotApiGetUpdatesDecoder.Decode(response.Content);

            UpdateGetUpdatesRequestOffset(updates);

            return updates;
        }

        public void SendMessage(TelegramBotOutgoingMessage message)
        {
            string uri = SendMessageUriString();
            string content = OutgoingMessageEncoder.Encode(message);

            PostRequest(uri, content);
        }

        private string SendMessageUriString()
        {
            return new UriStringBuilder(api_token, SEND_MESSAGE_ENDPOINT).Build();
        }

        private void UpdateGetUpdatesRequestOffset(List<TelegramBotUpdate> updates)
        {
            if (updates.Count == 0) {
                return;
            }

            List<long> ids = updates.Select(update => update.update_id).ToList();

            get_updates_offset = ids.Max() + 1;
        }

        private string GetUpdatesUriString(int timeout, long offset)
        {
            return new UriStringBuilder(api_token, GET_UPDATES_ENDPOINT).
                WithQueryParameter("timeout", timeout.ToString()).
                WithQueryParameter("offset", offset.ToString()).
                Build();
        }

        private HttpResponseMessageWrapper GetRequest(string uri)
        {
            return http_client.Get(uri);
        }

        private void PostRequest(string uri, string content)
        {
            http_client.Post(uri, content);
        }

    }
}
