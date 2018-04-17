using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;
using Xunit;

namespace SharBucketTests.V2.EndPoints
{
    public class UserEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private UserEndpoint userEndPoint;

        public UserEndPointTests()
        {
            sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            userEndPoint = sharpBucket.UserEndPoint();
        }

        [Fact]
        public void GetUser_FromLoggedUser_ShouldReturnAUser()
        {
            userEndPoint.ShouldNotBe(null);
            var user = userEndPoint.GetUser();
            user.ShouldNotBe(null);
        }
    }
}