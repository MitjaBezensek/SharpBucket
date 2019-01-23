using NUnit.Framework;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    internal class OAuthentication2Tests
    {
        private const int Expected = 300;

        [Test]
        public void OAuth2_RequestWithParameters_GetsPublicRepositories()
        {
            var sharpBucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth2();
            var publicRepos = sharpBucket.RepositoriesEndPoint().ListPublicRepositories(max: Expected);
            publicRepos.ShouldNotBe(null);
            publicRepos.Count.ShouldBe(Expected);
        }
    }
}