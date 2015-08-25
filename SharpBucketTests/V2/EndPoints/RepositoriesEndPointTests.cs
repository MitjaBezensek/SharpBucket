using NUnit.Framework;
using SharpBucket.V2;
using Shouldly;

namespace SharBucketTests.V2.EndPoints{
   [TestFixture]
   internal class RepositoriesEndPointTests{
      private SharpBucketV2 sharpBucket;

      [SetUp]
      public void Init(){
         sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithBasicAuthentication();
      }

      [Test]
      public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories(){
         var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
         repositoriesEndPoint.ShouldNotBe(null);

         var repositories = repositoriesEndPoint.ListRepositories("mirror");
         repositories.ShouldNotBe(null);
         repositories.Count.ShouldBeGreaterThan(10);
      }
   }
}