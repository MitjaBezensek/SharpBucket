using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharBucketTests.V2.EndPoints {
   [TestFixture]
   class UserEndPointTests {
      private SharpBucketV2 sharpBucket;
      private UserEndpoint userEndPoint;

      [SetUp]
      public void Init() {
         sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
         userEndPoint = sharpBucket.UserEndPoint();
      }

      [Test]
      public void GetUser_FromLoggedUser_ShouldReturnAUser() {
         userEndPoint.ShouldNotBe(null);
         var user = userEndPoint.GetUser();
         user.ShouldNotBe(null);
      }
   }
}