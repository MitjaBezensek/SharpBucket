using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket
{
    public static class SharpBucketRequesterExtensions
    {
        public static string Get(this ISharpBucketRequester requester, string relativeUrl)
        {
            return requester.Send(HttpMethod.Get, null, relativeUrl, null);
        }

        public static string Get(this ISharpBucketRequester requester, string relativeUrl, object requestParameters)
        {
            return requester.Send(HttpMethod.Get, null, relativeUrl, requestParameters);
        }

        public static Task<string> GetAsync(this ISharpBucketRequester requester, string relativeUrl, CancellationToken token)
        {
            return requester.SendAsync(HttpMethod.Get, null, relativeUrl, null, token);
        }

        public static Task<string> GetAsync(this ISharpBucketRequester requester, string relativeUrl, object requestParameters, CancellationToken token)
        {
            return requester.SendAsync(HttpMethod.Get, null, relativeUrl, requestParameters, token);
        }

        public static T Get<T>(this ISharpBucketRequester requester, string relativeUrl)
            where T : new()
        {
            return requester.Send<T>(HttpMethod.Get, null, relativeUrl, null);
        }

        public static T Get<T>(this ISharpBucketRequester requester, string relativeUrl, object requestParameters)
            where T : new()
        {
            return requester.Send<T>(HttpMethod.Get, null, relativeUrl, requestParameters);
        }

        public static Task<T> GetAsync<T>(this ISharpBucketRequester requester, string relativeUrl, CancellationToken token)
            where T : new()
        {
            return requester.SendAsync<T>(HttpMethod.Get, null, relativeUrl, null, token);
        }

        public static Task<T> GetAsync<T>(this ISharpBucketRequester requester, string relativeUrl, object requestParameters, CancellationToken token)
            where T : new()
        {
            return requester.SendAsync<T>(HttpMethod.Get, null, relativeUrl, requestParameters, token);
        }

        public static T Post<T>(this ISharpBucketRequester requester, T body, string relativeUrl)
            where T : new()
        {
            return requester.Send<T>(HttpMethod.Post, body, relativeUrl, null);
        }

        public static Task<T> PostAsync<T>(this ISharpBucketRequester requester, T body, string relativeUrl, CancellationToken token)
            where T : new()
        {
            return requester.SendAsync<T>(HttpMethod.Post, body, relativeUrl, null, token);
        }

        public static T Put<T>(this ISharpBucketRequester requester, T body, string relativeUrl)
            where T : new()
        {
            return requester.Send<T>(HttpMethod.Put, body, relativeUrl, null);
        }

        public static Task<T> PutAsync<T>(this ISharpBucketRequester requester, T body, string relativeUrl, CancellationToken token)
            where T : new()
        {
            return requester.SendAsync<T>(HttpMethod.Put, body, relativeUrl, null, token);
        }

        public static void Delete(this ISharpBucketRequester requester, string relativeUrl)
        {
            requester.Send(HttpMethod.Delete, null, relativeUrl, null);
        }

        public static Task DeleteAsync(this ISharpBucketRequester requester, string relativeUrl, CancellationToken token)
        {
            return requester.SendAsync(HttpMethod.Delete, null, relativeUrl, null, token);
        }
    }
}
