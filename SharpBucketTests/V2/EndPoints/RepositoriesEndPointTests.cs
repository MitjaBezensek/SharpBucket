using NUnit.Framework;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class RepositoriesEndPointTests
    {
        [Test]
        public void ListRepositories_WithNoMaxSet_ReturnsAtLeast10Repositories()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            repositoriesEndPoint.ShouldNotBe(null);

            var repositories = repositoriesEndPoint.ListRepositories(SampleRepositories.MERCURIAL_ACCOUNT_NAME);
            repositories.ShouldNotBe(null);
            repositories.Count.ShouldBeGreaterThan(10);
        }

        [Test]
        public void ListPublicRepositories_With30AsMax_Returns30PublicRepositories()
        {
            var repositoriesEndPoint = SampleRepositories.RepositoriesEndPoint;
            var publicRepositories = repositoriesEndPoint.ListPublicRepositories(30);
            publicRepositories.Count.ShouldBe(30);
            publicRepositories[5].full_name.ShouldBe("vetler/fhtmlmps");
        }
    }
}