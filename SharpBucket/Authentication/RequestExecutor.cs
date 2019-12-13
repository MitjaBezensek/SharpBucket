using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication
{
    internal abstract class RequestExecutor
    {
        private readonly JsonDeserializer jsonDeserializer = new JsonDeserializer();

        /// <summary>
        /// Configure a <see cref="IRestClient"/> instance to be compatible with the style of requests done by the <see cref="RequestExecutor"/>
        /// </summary>
        /// <param name="client">The client to configure</param>
        public virtual void ConfigureRestClient(IRestClient client)
        {
            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            client.FollowRedirects = false;
        }

        public string ExecuteRequest(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters)
        {
            var result = Execute(url, method, body, client, requestParameters);
            return result.Content;
        }

        public async Task<string> ExecuteRequestAsync(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters, CancellationToken token)
        {
            var result = await ExecuteAsync(url, method, body, client, requestParameters, token);
            return result.Content;
        }

        public T ExecuteRequest<T>(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters)
            where T : new()
        {
            var result = Execute(url, method, body, client, requestParameters);
            return jsonDeserializer.Deserialize<T>(result);
        }

        public async Task<T> ExecuteRequestAsync<T>(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters, CancellationToken token)
            where T : new()
        {
            var result = await ExecuteAsync(url, method, body, client, requestParameters, token);
            return jsonDeserializer.Deserialize<T>(result);
        }

        public IRestResponse ExecuteRequestNoRedirect(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters)
        {
            return ExecuteNoRedirect(url, method, body, client, requestParameters);
        }

        public Task<IRestResponse> ExecuteRequestNoRedirectAsync(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters, CancellationToken token)
        {
            return ExecuteNoRedirectAsync(url, method, body, client, requestParameters, token);
        }

        private IRestResponse Execute(
            string url,
            Method method,
            object body,
            IRestClient client,
            IDictionary<string, object> requestParameters)
        {
            var request = BuildRestRequest(url, method, body, requestParameters);
            var result = ExecuteRequestWithManualFollowRedirect(request, client);
            ThrowExceptionIfResponseIsInvalid(result);

            return result;
        }

        private async Task<IRestResponse> ExecuteAsync(
            string url,
            Method method,
            object body,
            IRestClient client,
            IDictionary<string, object> requestParameters,
            CancellationToken token)
        {
            var request = BuildRestRequest(url, method, body, requestParameters);
            var result = await ExecuteRequestWithManualFollowRedirectAsync(request, client, token);
            ThrowExceptionIfResponseIsInvalid(result);

            return result;
        }

        private IRestResponse ExecuteNoRedirect(
            string url,
            Method method,
            object body,
            IRestClient client,
            IDictionary<string, object> requestParameters)
        {
            var request = BuildRestRequest(url, method, body, requestParameters);
            var result = client.Execute(request);
            ThrowExceptionIfResponseIsInvalid(result);

            return result;
        }

        private async Task<IRestResponse> ExecuteNoRedirectAsync(
            string url,
            Method method,
            object body,
            IRestClient client,
            IDictionary<string, object> requestParameters,
            CancellationToken token)
        {
            var request = BuildRestRequest(url, method, body, requestParameters);
            var result = await client.ExecuteTaskAsync(request, token);
            ThrowExceptionIfResponseIsInvalid(result);

            return result;
        }

        private void ThrowExceptionIfResponseIsInvalid(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                // There is an issue that prevents the request to complete (timeout, request aborted, ...).
                // Throw the exception prepared by RestSharp
                throw new Exception(response.ErrorMessage, response.ErrorException);
            }

            if ((int)response.StatusCode >= 400)
            {
                // There is an issue which is described in the HTTP response.
                // Build an throw a BitBucketException, since the message should be provided by BitBucket.
                throw BuildBitbucketException(response);
            }
        }

        protected virtual BitbucketException BuildBitbucketException(IRestResponse response)
        {
            // response.ErrorException is not useful for caller in that case, so it's useless to transmit it as an inner exception
            throw new BitbucketException(response.StatusCode, response.Content);
        }

        private IRestRequest BuildRestRequest(string url, Method method, object body, IDictionary<string, object> requestParameters)
        {
            var request = new RestRequest(url, method);
            if (requestParameters != null)
            {
                foreach (var requestParameter in requestParameters)
                {
                    var key = requestParameter.Key;
                    var val = requestParameter.Value;

                    if (val is IEnumerable enumerable && !(enumerable is string))
                    {
                        foreach (var item in enumerable)
                            request.AddParameter(key, item);
                    }
                    else
                        request.AddParameter(key, val);
                }
            }

            if (method == Method.PUT || method == Method.POST)
            {
                AddBody(request, body);
            }

            return request;
        }

        protected abstract void AddBody(IRestRequest request, object body);

        private static IRestResponse ExecuteRequestWithManualFollowRedirect(IRestRequest request, IRestClient client)
        {
            var result = client.Execute(request);
            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                request = BuildRedirectedRestRequest(request, client, result);
                result = client.Execute(request);
            }

            return result;
        }

        private static async Task<IRestResponse>ExecuteRequestWithManualFollowRedirectAsync(IRestRequest request, IRestClient client, CancellationToken token)
        {
            var result = await client.ExecuteTaskAsync(request, token);
            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                request = BuildRedirectedRestRequest(request, client, result);
                result = await client.ExecuteTaskAsync(request, token);
            }

            return result;
        }

        private static IRestRequest BuildRedirectedRestRequest<TRestResponse>(IRestRequest request, IRestClient client,
            TRestResponse result) where TRestResponse : IRestResponse
        {
            var redirectUrl = GetRedirectUrl(result, client.BaseUrl.ToString());

            NameValueCollection queryValues;
            if (redirectUrl.Contains("?"))
            {
                var urlAndQuery = redirectUrl.Split('?');
                redirectUrl = urlAndQuery[0];
                queryValues = HttpUtility.ParseQueryString(urlAndQuery[1]);
            }
            else
            {
                queryValues = new NameValueCollection();
            }

            request = new RestRequest(redirectUrl, request.Method);
            foreach (var queryKey in queryValues.AllKeys)
            {
                request.AddQueryParameter(queryKey, queryValues[queryKey]);
            }

            return request;
        }

        private static string GetRedirectUrl(IRestResponse result, string requestBaseUrl)
        {
            var redirectUrl = result.Headers.Where(header => header.Name == "Location").Select(header => header.Value).First().ToString();
            if (redirectUrl.StartsWith(requestBaseUrl))
            {
                redirectUrl = redirectUrl.Remove(0, requestBaseUrl.Length);
            }
            return redirectUrl;
        }
    }
}