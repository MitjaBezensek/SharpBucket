using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication
{
    internal abstract class RequestExecutor
    {
        public T ExecuteRequest<T>(string url, Method method, object body, RestClient client, IDictionary<string, object> requestParameters)
            where T : new()
        {
            var request = new RestRequest(url, method);
            if (requestParameters != null)
            {
                foreach (var requestParameter in requestParameters)
                {
                    request.AddParameter(requestParameter.Key, requestParameter.Value);
                }
            }

            if (ShouldAddBody(method))
            {
                AddBody(request, body);
            }

            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            var result = ExecuteRequest<T>(method, client, request);

            if (result.ErrorException != null)
            {
                throw new WebException("REST client encountered an error: " + result.ErrorMessage, result.ErrorException);
            }
            // This is a hack in order to allow this method to work for simple types as well
            // one example of this is the GetRevisionRaw method
            if (RequestingSimpleType<T>())
            {
                return result.Content as dynamic;
            }
            return result.Data;
        }

        protected abstract void AddBody(RestRequest request, object body);

        private static IRestResponse<T> ExecuteRequest<T>(Method method, RestClient client, RestRequest request)
            where T : new()
        {
            IRestResponse<T> result;

            client.FollowRedirects = false;
            result = client.Execute<T>(request);
            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                var redirectUrl = GetRedirectUrl(result, client.BaseUrl.ToString());
                request = new RestRequest(redirectUrl, method);
                result = client.Execute<T>(request);
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

        private static bool ShouldAddBody(Method method)
        {
            return method == Method.PUT || method == Method.POST;
        }

        private static bool RequestingSimpleType<T>()
            where T : new()
        {
            return typeof(T) == typeof(object);
        }
    }
}