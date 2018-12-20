using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication
{
    internal abstract class RequestExecutor
    {
        public string ExecuteRequest(string url, Method method, object body, RestClient client, IDictionary<string, object> requestParameters)
        {
            var result = ExecuteRequest(url, method, body, client, requestParameters, client.Execute);
            return result.Content;
        }

        public T ExecuteRequest<T>(string url, Method method, object body, RestClient client, IDictionary<string, object> requestParameters)
            where T : new()
        {
            var result = ExecuteRequest(url, method, body, client, requestParameters, client.Execute<T>);
            return result.Data;
        }

        private TRestResponse ExecuteRequest<TRestResponse>(
            string url,
            Method method,
            object body,
            RestClient client,
            IDictionary<string, object> requestParameters,
            Func<RestRequest, TRestResponse> clientExecute)
            where TRestResponse : IRestResponse
        {
            var request = BuildRestRequest(url, method, body, requestParameters);

            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var result = ExecuteRequestWithManualFollowRedirect(request, client, clientExecute);

            if (result.ErrorException != null)
            {
                throw new WebException("REST client encountered an error: " + result.ErrorMessage, result.ErrorException);
            }

            return result;
        }

        private RestRequest BuildRestRequest(string url, Method method, object body, IDictionary<string, object> requestParameters)
        {
            var request = new RestRequest(url, method);
            if (requestParameters != null)
            {
                foreach (var requestParameter in requestParameters)
                {
                    request.AddParameter(requestParameter.Key, requestParameter.Value);
                }
            }

            if (method == Method.PUT || method == Method.POST)
            {
                AddBody(request, body);
            }

            return request;
        }

        protected abstract void AddBody(RestRequest request, object body);

        private static TRestResponse ExecuteRequestWithManualFollowRedirect<TRestResponse>(RestRequest request, RestClient client, Func<RestRequest, TRestResponse> clientExecute)
            where TRestResponse : IRestResponse
        {
            client.FollowRedirects = false;

            var result = clientExecute(request);
            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                var redirectUrl = GetRedirectUrl(result, client.BaseUrl.ToString());
                request = new RestRequest(redirectUrl, request.Method);
                result = clientExecute(request);
            }

            return result;
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