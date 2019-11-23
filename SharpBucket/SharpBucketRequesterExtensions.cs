using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket
{
    public static class SharpBucketRequesterExtensions
    {
        public static string Get(this ISharpBucketRequester requester, string relativeUrl, object requestParameters = null)
        {
            return requester.Send(null, HttpMethod.Get, relativeUrl, requestParameters);
        }

        public static async Task<string> GetAsync(this ISharpBucketRequester requester, string relativeUrl, object requestParameters = null, CancellationToken token = default)
        {
            return await requester.SendAsync(null, HttpMethod.Get, relativeUrl, requestParameters, token);
        }

        public static T Get<T>(this ISharpBucketRequester requester, string relativeUrl, object requestParameters = null)
            where T : new()
        {
            return requester.Send<T>(null, HttpMethod.Get, relativeUrl, requestParameters);
        }

        public static async Task<T> GetAsync<T>(this ISharpBucketRequester requester, string relativeUrl, object requestParameters = null, CancellationToken token = default)
            where T : new()
        {
            return await requester.SendAsync<T>(null, HttpMethod.Get, relativeUrl, requestParameters, token);
        }

        public static T Post<T>(this ISharpBucketRequester requester, T body, string relativeUrl)
            where T : new()
        {
            return requester.Send<T>(body, HttpMethod.Post, relativeUrl);
        }

        public static async Task<T> PostAsync<T>(this ISharpBucketRequester requester, T body, string relativeUrl = null, CancellationToken token = default)
            where T : new()
        {
            return await requester.SendAsync<T>(body, HttpMethod.Post, relativeUrl, null, token);
        }

        public static T Put<T>(this ISharpBucketRequester requester, T body, string relativeUrl)
            where T : new()
        {
            return requester.Send<T>(body, HttpMethod.Put, relativeUrl);
        }

        public static async Task<T> PutAsync<T>(this ISharpBucketRequester requester, T body, string relativeUrl = null, CancellationToken token = default)
            where T : new()
        {
            return await requester.SendAsync<T>(body, HttpMethod.Put, relativeUrl, null, token);
        }

        public static void Delete(this ISharpBucketRequester requester, string relativeUrl)
        {
            requester.Send(null, HttpMethod.Delete, relativeUrl);
        }

        public static async Task DeleteAsync(this ISharpBucketRequester requester, string relativeUrl = null, CancellationToken token = default)
        {
            await requester.SendAsync(null, HttpMethod.Delete, relativeUrl, null, token);
        }
    }
}
