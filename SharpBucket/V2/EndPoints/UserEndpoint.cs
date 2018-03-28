using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class UserEndpoint : EndPoint
    {
        public UserEndpoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "user/")
        {
        }

        /// <summary>
        /// Returns the currently logged in user.
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            return _sharpBucketV2.Get<User>(null, _baseUrl);
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
    }
}