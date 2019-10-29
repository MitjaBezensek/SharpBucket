using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using SharpBucket.V2;

namespace SharpBucket.Authentication
{
    public abstract class Authenticate
    {
        private IRestClient _client;

        protected virtual IRestClient Client
        {
            get => _client;
            set
            {
                this.RequestExecutor.ConfigureRestClient(value);
                _client = value;
            }
        }

        internal RequestExecutor RequestExecutor { get; set; } = new RequestExecutorV2(); // Use V2 by default since V1 should disappear soon

        public virtual string GetResponse(string url, Method method, object body, IDictionary<string, object> requestParameters)
        {
            return RequestExecutor.ExecuteRequest(url, method, body, Client, requestParameters);
        }

        public virtual async Task<string> GetResponseAsync(string url, Method method, object body, IDictionary<string, object> requestParameters, CancellationToken token)
        {
            return await RequestExecutor.ExecuteRequestAsync(url, method, body, Client, requestParameters, token);
        }

        public virtual T GetResponse<T>(string url, Method method, object body, IDictionary<string, object> requestParameters)
            where T : new()
        {
            return RequestExecutor.ExecuteRequest<T>(url, method, body, Client, requestParameters);
        }

        public virtual async Task<T> GetResponseAsync<T>(string url, Method method, object body, IDictionary<string, object> requestParameters, CancellationToken token)
            where T : new()
        {
            return await RequestExecutor.ExecuteRequestAsync<T>(url, method, body, Client, requestParameters, token);
        }

        public virtual Uri GetRedirectLocation(string url, Method method, object body, IDictionary<string, object> requestParameters)
        {
            var response = RequestExecutor.ExecuteRequestNoRedirect(url, method, body, Client, requestParameters);
            return ExtractRedirectLocation(response);
        }

        public virtual async Task<Uri> GetRedirectLocationAsync(string url, Method method, object body, IDictionary<string, object> requestParameters, CancellationToken token)
        {
            var response = await RequestExecutor.ExecuteRequestNoRedirectAsync(url, method, body, Client, requestParameters, token);
            return ExtractRedirectLocation(response);
        }

        private Uri ExtractRedirectLocation(IRestResponse response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.Redirect)
            {
                throw new Exception($"Response is not a redirect. (Response status code: {response.StatusCode})");
            }
            var redirectUrl = response.Headers.Where(header => header.Name == "Location").Select(header => header.Value).First().ToString();
            return new Uri(redirectUrl);
        }
    }
}