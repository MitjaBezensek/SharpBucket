using NUnit.Framework;
using SharpBucket.V2;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    public class NoAuthenticationTests
    {
        [Test]
        public void Client_WithNoAuth_ShouldRetrieveTortoise()
        {
            var client = new SharpBucketV2();
            var ep = client.RepositoriesEndPoint();
            var repo = ep.RepositoryResource("tortoisehg", "thg").GetRepository();

            repo.ShouldNotBeNull();
            repo.full_name.ShouldBe("tortoisehg/thg");
        }

        [Test]
        public void Client_WithNoAuth_ShouldReadPublicRepos()
        {
            var client = new SharpBucketV2();
            var ep = client.RepositoriesEndPoint();
            var repos = ep.ListPublicRepositories(5);

            repos.Count.ShouldBe(5);
        }
    }
}
