using SharpBucket.V2.Pocos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

namespace SharpBucket.V2.EndPoints
{
    public class EndPoint
    {
        // vanilla page length in many cases is 10, requiring lots of requests for larger collections
        private const int DEFAULT_PAGE_LEN = 50;

        protected readonly SharpBucketV2 _sharpBucketV2;
        protected readonly string _baseUrl;

        public EndPoint(SharpBucketV2 sharpBucketV2, string resourcePath)
        {
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = resourcePath;
        }

        /// <summary>
        /// Generator that allows lazy access to paginated resources.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="overrideUrl"></param>
        /// <param name="pageLen"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        private IEnumerable<List<TValue>> IteratePages<TValue>(string overrideUrl, int pageLen = DEFAULT_PAGE_LEN, IDictionary<string, object> requestParameters = null)
        {
            Debug.Assert(!String.IsNullOrEmpty(overrideUrl));
            Debug.Assert(!overrideUrl.Contains("?"));

            if (requestParameters == null)
            {
                requestParameters = new Dictionary<string, object>();
            }

            requestParameters["pagelen"] = pageLen;

            IteratorBasedPage<TValue> response;
            int page = 1;
            do
            {
                response = _sharpBucketV2.Get(new IteratorBasedPage<TValue>(), overrideUrl.Replace(SharpBucketV2.BITBUCKET_URL, ""), requestParameters);
                if (response == null)
                {
                    break;
                }

                yield return response.values;

                requestParameters["page"] = ++page;
            }
            while (!String.IsNullOrEmpty(response.next));
        }

        /// <summary>
        /// Returns a list of paginated values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="overrideUrl">The override URL.</param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        /// <exception cref="System.Net.WebException">Thrown when the server fails to respond.</exception>
        protected List<TValue> GetPaginatedValues<TValue>(string overrideUrl, int max = 0, IDictionary<string, object> requestParameters = null)
        {
            bool isMaxConstrained = max > 0;

            int pageLen = (isMaxConstrained && max < DEFAULT_PAGE_LEN) ? max : DEFAULT_PAGE_LEN;

            List<TValue> values = new List<TValue>();

            foreach (var page in IteratePages<TValue>(overrideUrl, pageLen, requestParameters))
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