using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints {
    public class UserEndpoint : EndPoint {
        public UserEndpoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "user/") {
        }

        public User GetUser() {
            return _sharpBucketV2.Get<User>(null, _baseUrl);
        }
    }
}