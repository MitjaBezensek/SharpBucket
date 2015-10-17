using NUnit.Framework;
using Shouldly;

namespace SharBucketTests.Authentication {
    [TestFixture]
    internal class OAuthentication2Tests {
        [Test]
        public void OAuth2_RequestWithParameters_GetsPublicRepositories() {
            var sharpbucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth2();
            var publicRepos = sharpbucket.RepositoriesEndPoint().ListPublicRepositories(max: 50);
            publicRepos.ShouldNotBe(null);
            publicRepos.Count.ShouldBe(50);
        }
    }
}