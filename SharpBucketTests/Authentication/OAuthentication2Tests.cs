using Shouldly;
using Xunit;

namespace SharBucketTests.Authentication
{
    
    public class OAuthentication2Tests
    {
        private const int Expected = 300;

        [Fact]
        public void OAuth2_RequestWithParameters_GetsPublicRepositories()
        {
            var sharpbucket = TestHelpers.GetV2ClientAuthenticatedWithOAuth();
            var publicRepos = sharpbucket.RepositoriesEndPoint().ListPublicRepositories(max: Expected);
            publicRepos.ShouldNotBe(null);
            publicRepos.Count.ShouldBe(Expected);
        }
    }
}