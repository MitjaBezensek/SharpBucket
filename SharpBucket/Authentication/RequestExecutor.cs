using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication
{
    internal abstract class RequestExecutor
    {
        public string ExecuteRequest(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters)
        {
            var result = ExecuteRequest(url, method, body, client, requestParameters, client.Execute);
            return result.Content;
        }

        public T ExecuteRequest<T>(string url, Method method, object body, IRestClient client, IDictionary<string, object> requestParameters)
            where T : new()
        {
            var result = ExecuteRequest(url, method, body, client, requestParameters, client.Execute<T>);
            return result.Data;
        }

        private TRestResponse ExecuteRequest<TRestResponse>(
            string url,
            Method method,
            object body,
            IRestClient client,
            IDictionary<string, object> requestParameters,
            Func<IRestRequest, TRestResponse> clientExecute)
            where TRestResponse : IRestResponse
        {
            var request = BuildRestRequest(url, method, body, requestParameters);

            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item
            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            var result = ExecuteRequestWithManualFollowRedirect(request, client, clientExecute);
            if (!result.IsSuccessful)
            {
                throw BuildBitbucketException(result);
            }

            return result;
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

                    if (val is IEnumerable && !(val is string))
                    {
                        foreach (var item in (IEnumerable)val)
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

        private static TRestResponse ExecuteRequestWithManualFollowRedirect<TRestResponse>(IRestRequest request, IRestClient client, Func<IRestRequest, TRestResponse> clientExecute)
            where TRestResponse : IRestResponse
        {
            client.FollowRedirects = false;

            var result = clientExecute(request);
            if (result.StatusCode == HttpStatusCode.Redirect)
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