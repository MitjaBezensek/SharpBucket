using NUnit.Framework;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    internal class OAuthenticationTests
    {
        private const int Expected = 300;

        [Test]
        public void OAuth_RequestWithParameters_GetsPublicRepositories()
        {
            var sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth1();
            var publicRepos = sharpBucket.RepositoriesEndPoint().ListPublicRepositories(max: Expected);
            publicRepos.ShouldNotBe(null);
            publicRepos.Count.ShouldBe(Expected);
        }
    }
}