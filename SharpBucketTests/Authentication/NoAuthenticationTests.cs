using NUnit.Framework;
using SharpBucket.V2;
using SharpBucketTests.V2;
using Shouldly;
using System.Linq;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    public class NoAuthenticationTests
    {
        [Test]
        public void NoAuthentication_NotExplicitlyCalled_ShouldReadPublicRepos()
        {
            var sharpBucket = new SharpBucketV2();
            var ep = sharpBucket.RepositoriesEndPoint();
            var repos = ep.ListPublicRepositories(5);

            repos.Count.ShouldBe(5);
        }

        [Test]
        public void NoAuthentication_ExplicitlyCalledAfterAnEffectiveAuthentication_ShouldReadPublicReposButNotPrivateOnes()
        {
            var privateRepo = SampleRepositories.PrivateTestRepository.GetRepository();
            var publicRepo = SampleRepositories.EmptyTestRepository.GetRepository();

            var sharpBucket = new SharpBucketV2();
            var repositoriesEndPoint = sharpBucket.RepositoriesEndPoint();

            sharpBucket.OAuth2ClientCredentials(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var accountRepos = repositoriesEndPoint.RepositoriesResource(TestHelpers.AccountName).ListRepositories();
            accountRepos.ShouldNotBe(null);
            accountRepos.Any(p => p.name == privateRepo.name).ShouldBe(true);
            accountRepos.Any(p => p.is_private == true).ShouldBe(true);
            accountRepos.Any(p => p.is_private == false && p.name == publicRepo.name).ShouldBe(true);

            sharpBucket.NoAuthentication();
            accountRepos = repositoriesEndPoint.RepositoriesResource(TestHelpers.AccountName).ListRepositories();
            accountRepos.ShouldNotBe(null);
            accountRepos.Any(p => p.name == privateRepo.name).ShouldBe(false);
            accountRepos.Any(p => p.is_private == true).ShouldBe(false);
            accountRepos.Any(p => p.is_private == false && p.name == publicRepo.name).ShouldBe(true);
        }
    }
}
