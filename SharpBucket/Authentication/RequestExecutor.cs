using System.Collections.Generic;
using System.Linq;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication
{
    internal class RequestExecutor
    {
        internal const string BITBUCKET_URL_V1 = "https://bitbucket.org/api/1.0";
        internal const string BITBUCKET_URL_V2 = "https://api.bitbucket.org/2.0";
        public static T ExecuteRequest<T>(string url, Method method, T body, RestClient client, IDictionary<string, object> requestParameters)
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
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(body);
            }

            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            var result = ExectueRequest<T>(method, client, request);

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

        private static IRestResponse<T> ExectueRequest<T>(Method method, RestClient client, RestRequest request)
            where T : new()
        {
            IRestResponse<T> result;

            client.FollowRedirects = false;
            result = client.Execute<T>(request);
            if (result.StatusCode == HttpStatusCode.Redirect)
            {
                var redirectUrl = GetRedirectUrl(result);
                request = new RestRequest(redirectUrl, method);
                result = client.Execute<T>(request);
            }
            return result;
        }

        private static string GetRedirectUrl(IRestResponse result)
        {
            var redirectUrl = result.Headers.Where(header => header.Name == "Location").Select(header => header.Value).First().ToString();
            if (redirectUrl.Contains(BITBUCKET_URL_V1))
            {
                return redirectUrl.Replace(BITBUCKET_URL_V1, "");
            }
            else
            {
                return redirectUrl.Replace(BITBUCKET_URL_V2, "");
            }
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