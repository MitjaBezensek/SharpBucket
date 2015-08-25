using SharpBucket.V2.Pocos;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SharpBucket.V2.EndPoints {
    public class EndPoint {
        protected readonly SharpBucketV2 _sharpBucketV2;
        protected readonly string _baseUrl;

        public EndPoint (SharpBucketV2 sharpBucketV2, string resourcePath) {
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = resourcePath;
        }

        /// <summary>
        /// Iterates through a series of paginated values.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="overrideUrl"></param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <returns></returns>
        protected List<TValue> GetPaginatedValues<TValue> (string overrideUrl, int max = 0) {
            // default page length in many cases is 10, requiring lots of requests for larger collections
            const int DEFAULT_PAGE_LEN = 100;
            int pageLen = (max > 0 && max < DEFAULT_PAGE_LEN) ? max : DEFAULT_PAGE_LEN;

            Debug.Assert(!overrideUrl.Contains("?"));
            overrideUrl += "?pagelen=" + pageLen;

            IteratorBasedPage<TValue> response = _sharpBucketV2.Get(new IteratorBasedPage<TValue>(), overrideUrl);
            List<TValue> values = null;

            if (response != null && response.values != null) {

                values = response.values;

                while (!String.IsNullOrEmpty(response.next) && (max <= 0 || values.Count < max)) {
                    overrideUrl = response.next.Replace(SharpBucketV2.BITBUCKET_URL, "");
                    Debug.Assert(overrideUrl.Length < response.next.Length);

                    response = _sharpBucketV2.Get(new IteratorBasedPage<TValue>(), overrideUrl);
                    
                    // should this case throw?
                    if (response == null) { break; }

                    // if BitBucket is providing what should be a valid "next" URL,
                    // why is this ever null?
                    if (response.values != null) {
                        values.AddRange(response.values);
                    }
                }
            }

            if (values != null && max > 0 && values.Count > max) {
                values.RemoveRange(max - 1, values.Count - max);
            }

            return values;
        }
    }
}
