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
        private static IEnumerable<List<TValue>> IteratePages<TValue>(SharpBucketV2 sharpBucketV2, string overrideUrl, int pageLen = DEFAULT_PAGE_LEN, IDictionary<string, object> requestParameters = null)
        {
            Debug.Assert(!string.IsNullOrEmpty(overrideUrl));
            Debug.Assert(!overrideUrl.Contains("?"));

            overrideUrl = overrideUrl.Replace(SharpBucketV2.BITBUCKET_URL, "");

            if (requestParameters == null)
            {
                requestParameters = new Dictionary<string, object>();
            }

            requestParameters["pagelen"] = pageLen;

            while (true)
            {
                var response = sharpBucketV2.Get<IteratorBasedPage<TValue>>(overrideUrl, requestParameters);
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
        /// Returns a list of paginated values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="sharpBucketV2"></param>
        /// <param name="overrideUrl">The override URL.</param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        public static List<TValue> GetPaginatedValues<TValue>(this SharpBucketV2 sharpBucketV2, string overrideUrl, int max = 0, IDictionary<string, object> requestParameters = null)
        {
            var isMaxConstrained = max > 0;

            var pageLen = (isMaxConstrained && max < DEFAULT_PAGE_LEN) ? max : DEFAULT_PAGE_LEN;

            var values = new List<TValue>();

            foreach (var page in IteratePages<TValue>(sharpBucketV2, overrideUrl, pageLen, requestParameters))
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
    }
}
