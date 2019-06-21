using System.Linq;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucketTests.V2;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    internal class OAuth2Tests
    {
        [Test]
        public void OAuth2ClientCredentials_AllowToListPrivateRepositories()
        {
            var privateRepo = SampleRepositories.PrivateTestRepository.GetRepository();

            var sharpBucket = new SharpBucketV2();
            sharpBucket.OAuth2ClientCredentials(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var accountRepos = sharpBucket.RepositoriesEndPoint().ListRepositories(TestHelpers.AccountName);
            accountRepos.ShouldNotBe(null);
            accountRepos.Any(p => p.is_private == true && p.name == privateRepo.name).ShouldBe(true);
        }
    }
}