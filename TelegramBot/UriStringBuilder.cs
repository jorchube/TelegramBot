using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelegramBotApp
{
    public class UriStringBuilder
    {
        string api_token;
        string endpoint;
        Dictionary<string, string> query_parameters;

        public UriStringBuilder(string api_token, string endpoint)
        {
            query_parameters = new Dictionary<string, string>();

            this.api_token = api_token;
            this.endpoint = endpoint;
        }

        public UriStringBuilder WithQueryParameter(string name, string value)
        {
            query_parameters.Add(name, value);

            return this;
        }

        public string Build()
        {
            string uri = "https://api.telegram.org/bot" + api_token + "/" + endpoint;

            uri = AddQueryParameters(uri);

            return uri;
        }

        string AddQueryParameters(string uri)
        {
            string new_uri;

            if (query_parameters.Count == 0) {
                return uri;
            }

            new_uri = uri + "?";

            foreach (KeyValuePair<string, string> pair in query_parameters) {
                new_uri = new_uri + pair.Key + "=" + pair.Value;
                if (!pair.Equals(query_parameters.Last())) {
                    new_uri += "&";
                }
            }

            return new_uri;
        }
    }
}
