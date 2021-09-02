#if CS_8
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2
{
    internal static class AsyncPaginatedListIterator
    {
        // vanilla page length in many cases is 10, requiring lots of requests for larger collections
        private const int DEFAULT_PAGE_LEN = 50;

        /// <summary>
        /// Generator that allows lazy access to paginated resources.
        /// </summary>
        private static async IAsyncEnumerable<List<TValue>> IteratePagesAsync<TValue>(
            ISharpBucketRequesterV2 sharpBucketRequesterV2,
            string overrideUrl,
            int pageLen,
            IDictionary<string, object> requestParameters,
            [EnumeratorCancellation]CancellationToken token)
        {
            Debug.Assert(!string.IsNullOrEmpty(overrideUrl));
            Debug.Assert(!overrideUrl.Contains("?"));

            overrideUrl = overrideUrl.Replace(SharpBucketV2.BITBUCKET_URL, "");

            requestParameters ??= new Dictionary<string, object>();
            requestParameters["pagelen"] = pageLen;

            while (true)
            {
                var response = await sharpBucketRequesterV2.GetAsync<IteratorBasedPage<TValue>>(overrideUrl, requestParameters, token: token);
                if (response == null)
                {
                    break;
                }

                yield return response.values;

                if (string.IsNullOrWhiteSpace(response.next))
                {
                    break;
                }

                var uri = new Uri(response.next);
                var parsedQuery = HttpUtility.ParseQueryString(uri.Query);
                requestParameters = parsedQuery.AllKeys.ToDictionary<string, string, object>(key => key, parsedQuery.Get);
            }
        }

        /// <summary>
        /// Returns an enumeration of paginated values. The pages are requested lazily while iterating on the values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sharpBucketRequesterV2"></param>
        /// <param name="overrideUrl">The override URL.</param>
        /// <param name="requestParameters"></param>
        /// <param name="pageLen">The size of a page.</param>
        /// <param name="token"></param>
        /// <returns>A lazy enumerable over the values.</returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        public static async IAsyncEnumerable<TValue> EnumeratePaginatedValuesAsync<TValue>(
            this ISharpBucketRequesterV2 sharpBucketRequesterV2,
            string overrideUrl,
            IDictionary<string, object> requestParameters,
            int? pageLen,
            [EnumeratorCancellation]CancellationToken token)
        {
            await foreach (var page in IteratePagesAsync<TValue>(sharpBucketRequesterV2, overrideUrl, pageLen ?? DEFAULT_PAGE_LEN, requestParameters, token))
            {
                if (page == null)
                {
                    yield break;
                }
                foreach (var item in page)
                {
                    yield return item;
                }
            }
        }
    }
}

#endif