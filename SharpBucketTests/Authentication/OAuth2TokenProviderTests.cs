using System.Linq;
using SharpBucket.Authentication;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    using NUnit.Framework;

    [TestFixture]
    public class OAuth2TokenProviderTests
    {
        [Test]
        public void RefreshToken_AskToRefreshATokenRetrievedWithGetToken_GetANewToken()
        {
            var oauth2TokenProvider = new OAuth2TokenProvider(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var token = oauth2TokenProvider.GetToken();

            var refreshedToken = oauth2TokenProvider.RefreshToken(token);

            refreshedToken.ShouldNotBeNull();
            refreshedToken.AccessToken.ShouldNotBe(token.AccessToken, "AccessToken should have been changed");
            refreshedToken.RefreshToken.ShouldBe(token.RefreshToken, "RefreshToken should note have been changed");
            refreshedToken.TokenType.ShouldBe(token.TokenType, "TokenType should note have been changed");

            // the scopes order is not maintain by that operation, so we should parse and order them to compare the two lists
            var originalScopes = token.Scopes.Split(' ').ToList();
            originalScopes.Sort();
            var refreshedScopes = refreshedToken.Scopes.Split(' ').ToList();
            refreshedScopes.Sort();
            refreshedScopes.ShouldBe(originalScopes, "Scopes should note have been changed");
        }
    }
}