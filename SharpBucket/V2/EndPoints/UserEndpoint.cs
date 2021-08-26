using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class UserEndpoint : EndPoint
    {
        public UserEndpoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "user")
        {
        }

        public User GetUser()
        {
            return SharpBucketV2.Get<User>(BaseUrl);
        }

        public async Task<User> GetUserAsync(CancellationToken token = default)
        {
            return await SharpBucketV2.GetAsync<User>(BaseUrl, token);
        }
    }
}