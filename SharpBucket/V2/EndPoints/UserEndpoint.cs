using System.Collections.Generic;
using Serilog;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class UserEndpoint : EndPoint
    {
        public UserEndpoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "user/")
        {
        }
        public UserEndpoint(ISharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "user/")
        {
        }

        public User GetUser()
        {
            return _sharpBucketV2.Get<User>(null, _baseUrl);
        }

        /// <summary>
        /// Get With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public User GetUser(ILogger logger)
        {
            return _sharpBucketV2.Get<User>(logger, null, _baseUrl);
        }

        /// <summary>
        /// Returns all the authenticated user's email addresses. Both confirmed and unconfirmed.
        /// </summary>
        /// <returns></returns>
        public List<Email> GetEmails(int max = 0)
        {
            var overrideUrl = _baseUrl + "emails/";
            return GetPaginatedValues<Email>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public object GetEmails(ILogger logger, int max = 0)
        {
            var overrideUrl = _baseUrl + "emails/";
            return GetPaginatedValues<Email>(logger, overrideUrl, max);
        }
    }
}