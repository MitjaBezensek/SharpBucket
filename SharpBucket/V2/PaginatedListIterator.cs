using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2
{
    internal static class PaginatedListIterator
    {
        // vanilla page length in many cases is 10, requiring lots of requests for larger collections
        private const int DEFAULT_PAGE_LEN = 50;

        /// <summary>
        /// Generator that allows lazy access to paginated resources.
        /// </summary>
        private static IEnumerable<List<TValue>> IteratePages<TValue>(ISharpBucketRequesterV2 sharpBucketRequesterV2, string relativeUrl, int pageLen = DEFAULT_PAGE_LEN, IDictionary<string, object> requestParameters = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(relativeUrl));
            Debug.Assert(!relativeUrl.Contains("?"));

            relativeUrl = relativeUrl.Replace(SharpBucketV2.BITBUCKET_URL, "");

            if (requestParameters == null)
            {
                requestParameters = new Dictionary<string, object>();
            }

            requestParameters["pagelen"] = pageLen;

            while (true)
            {
                var response = sharpBucketRequesterV2.Get<IteratorBasedPage<TValue>>(relativeUrl, requestParameters);
                if (response == null)
                {
                    break;
                }

                yield return response.values;

                // stop iteration if there is no more next
                // or if we have a size and we detect we are on the last page
                // note that on some routes an issue in bitbucket cause next to be proposed while we are already on the last page.
                if (string.IsNullOrWhiteSpace(response.next)
                    || (response.size != null
                        && response.page != null
                        && response.size.Value < (ulong)pageLen * (ulong)response.page.Value))
                {
                    break;
                }

                var uri = new Uri(response.next);
                var parsedQuery = HttpUtility.ParseQueryString(uri.Query);
                requestParameters = parsedQuery.AllKeys.ToDictionary<string, string, object>(key => key, parsedQuery.Get);
            }
        }

        /// <summary>
        /// Returns a list of paginated values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sharpBucketRequester"></param>
        /// <param name="relativeUrl">The relative URL to the paginated resource to call.</param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        public static List<TValue> GetPaginatedValues<TValue>(this ISharpBucketRequesterV2 sharpBucketRequesterV2, string relativeUrl, int max = 0, IDictionary<string, object> requestParameters = null)
        {
            var isMaxConstrained = max > 0;

            var pageLen = (isMaxConstrained && max < DEFAULT_PAGE_LEN) ? max : DEFAULT_PAGE_LEN;

            var values = new List<TValue>();

            foreach (var page in IteratePages<TValue>(sharpBucketRequesterV2, relativeUrl, pageLen, requestParameters))
            {
                if (page == null)
                {
                    break;
                }

                if (isMaxConstrained &&
                    values.Count + page.Count >= max)
                {
                    values.AddRange(page.GetRange(0, max - values.Count));
                    Debug.Assert(values.Count == max);
                    break;
                }

                values.AddRange(page);
            }

            return values;
        }

        /// <summary>
        /// Returns an enumeration of paginated values. The pages are requested lazily while iterating on the values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sharpBucketRequester"></param>
        /// <param name="relativeUrl">The relative URL to the paginated resource to call.</param>
        /// <param name="requestParameters"></param>
        /// <param name="pageLen">The size of a page.</param>
        /// <returns>A lazy enumerable over the values.</returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        public static IEnumerable<TValue> EnumeratePaginatedValues<TValue>(this ISharpBucketRequesterV2 sharpBucketRequesterV2, string relativeUrl, IDictionary<string, object> requestParameters = null, int? pageLen = null)
        {
            return IteratePages<TValue>(sharpBucketRequesterV2, relativeUrl, pageLen ?? DEFAULT_PAGE_LEN, requestParameters)
                .TakeWhile(page => page != null)
                .SelectMany(page => page);
        }
    }
}
