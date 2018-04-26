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
            return Get<User>(null, _baseUrl);
        }
    }
}