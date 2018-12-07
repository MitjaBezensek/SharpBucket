using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class RepositoriesEndPointTests
    {
        private SharpBucketV2 sharpBucket;
        private RepositoriesEndPoint repositoriesEndPoint;

        [SetUp]
        public void Init()
        {
            sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();
        }

        [Test]
        public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories()
        {
            repositoriesEndPoint.ShouldNotBe(null);

            var repositories = repositoriesEndPoint.ListRepositories("mirror");
            repositories.ShouldNotBe(null);
            repositories.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public void ListPublicRepositories_With30AsMax_Returns30PublicRepositories()
        {
            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(30);
            publicRepositories.Count.ShouldBe(30);
            publicRepositories[5].full_name.ShouldBe("vetler/fhtmlmps");
        }
    }
}