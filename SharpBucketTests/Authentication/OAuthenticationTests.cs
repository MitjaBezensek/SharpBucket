using NUnit.Framework;
using Shouldly;

namespace SharBucketTests.Authentication
{
    [TestFixture]
    internal class OAuthenticationTests
    {
        private const int Expected = 300;

        [Test]
        public void OAuth_RequestWithParameters_GetsPublicRepositories()
        {
            var sharpbucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            var publicRepos = sharpbucket.RepositoriesEndPoint().ListPublicRepositories(max: Expected);
            publicRepos.ShouldNotBe(null);
            publicRepos.Count.ShouldBe(Expected);
        }
    }
}