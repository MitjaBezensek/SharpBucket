using System.Collections.Generic;


namespace SharpBucket.V2.EndPoints
{
    public class EndPoint
    {
        protected readonly SharpBucketV2 _sharpBucketV2;
        protected readonly string _baseUrl;

        public EndPoint(SharpBucketV2 sharpBucketV2, string resourcePath)
        {
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = resourcePath;
        }

        /// <summary>
        /// Returns a list of paginated values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="overrideUrl">The override URL.</param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        protected List<TValue> GetPaginatedValues<TValue>(string overrideUrl, int max = 0, IDictionary<string, object> requestParameters = null)
        {
            return _sharpBucketV2.GetPaginatedValues<TValue>(overrideUrl, max, requestParameters);
        }
    }
}