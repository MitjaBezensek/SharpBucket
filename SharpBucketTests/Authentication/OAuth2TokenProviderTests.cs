using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using SharpBucket.Authentication;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    public class OAuth2TokenProviderTests
    {
        [Test]
        [Ignore("Excluded from automatic runs. Need manual operations.")]
        public void GetAuthorizationCodeToken_CallToAuthorizeIsNotImplementedAndMustBeDoneOutsideManually_GetAValidToken()
        {
            var clientId = TestHelpers.OAuthConsumerKey;
            var url = $"https://bitbucket.org/site/oauth2/authorize?client_id={clientId}&response_type=code";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else
            {
                throw new PlatformNotSupportedException("look here to complete that implementation if needed: https://github.com/dotnet/corefx/issues/10361");
            }
            var code = $"TODO: replace this by your code delivered to {clientId} with debugger";

            // PLACE A BREAKPOINT HERE, to affect the code variable before going further
            var oauth2TokenProvider = new OAuth2TokenProvider(clientId, TestHelpers.OAuthConsumerSecretKey);
            var token = oauth2TokenProvider.GetAuthorizationCodeToken(code);

            token.ShouldNotBeNull();
            token.AccessToken.ShouldNotBeNullOrEmpty(nameof(token.AccessToken));
            token.RefreshToken.ShouldNotBeNullOrEmpty(nameof(token.RefreshToken));
            token.ExpiresIn.ShouldBeGreaterThan(0, nameof(token.ExpiresIn));
            token.ExpiresAt.ShouldBeGreaterThan(DateTime.UtcNow);
            token.Scopes.ShouldNotBeNullOrEmpty(nameof(token.Scopes));
            token.TokenType.ShouldBe("bearer", nameof(token.TokenType));
        }

        [Test]
        [Ignore("Excluded from automatic runs. We recommend providing your real password in the clear only when needed to do a manual run of this test.")]
        public void GetResourceOwnerPasswordCredentialsToken_OAuthConsumerIsNotNecessaryTheSameThanTheOneForWhichWeGetTheCredentials_GetAValidToken()
        {
            var oauth2TokenProvider = new OAuth2TokenProvider(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var token = oauth2TokenProvider.GetResourceOwnerPasswordCredentialsToken(TestHelpers.UserName, TestHelpers.Password);

            token.ShouldNotBeNull();
            token.AccessToken.ShouldNotBeNullOrEmpty(nameof(token.AccessToken));
            token.RefreshToken.ShouldNotBeNullOrEmpty(nameof(token.RefreshToken));
            token.ExpiresIn.ShouldBeGreaterThan(0, nameof(token.ExpiresIn));
            token.ExpiresAt.ShouldBeGreaterThan(DateTime.UtcNow);
            token.Scopes.ShouldNotBeNullOrEmpty(nameof(token.Scopes));
            token.TokenType.ShouldBe("bearer", nameof(token.TokenType));
        }

        [Test]
        public void GetClientCredentialsToken_TestConsumer_GetAValidToken()
        {
            var oauth2TokenProvider = new OAuth2TokenProvider(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var token = oauth2TokenProvider.GetClientCredentialsToken();

            token.ShouldNotBeNull();
            token.AccessToken.ShouldNotBeNullOrEmpty(nameof(token.AccessToken));
            token.RefreshToken.ShouldNotBeNullOrEmpty(nameof(token.RefreshToken));
            token.ExpiresIn.ShouldBeGreaterThan(0, nameof(token.ExpiresIn));
            token.ExpiresAt.ShouldBeGreaterThan(DateTime.UtcNow);
            token.Scopes.ShouldNotBeNullOrEmpty(nameof(token.Scopes));
            token.TokenType.ShouldBe("bearer", nameof(token.TokenType));
        }

        [Test]
        public void RefreshToken_AskToRefreshATokenRetrievedWithGetToken_GetANewToken()
        {
            var oauth2TokenProvider = new OAuth2TokenProvider(TestHelpers.OAuthConsumerKey, TestHelpers.OAuthConsumerSecretKey);
            var token = oauth2TokenProvider.GetClientCredentialsToken();

            var refreshedToken = oauth2TokenProvider.RefreshToken(token);

            refreshedToken.ShouldNotBeNull();
            refreshedToken.AccessToken.ShouldNotBe(token.AccessToken, "AccessToken should have been changed");
            refreshedToken.RefreshToken.ShouldBe(token.RefreshToken, "RefreshToken should not have been changed");
            refreshedToken.TokenType.ShouldBe(token.TokenType, "TokenType should not have been changed");

            // The order of scopes is not maintained, so we should parse and order them to compare the two lists.
            var originalScopes = token.Scopes.Split(' ').ToList();
            originalScopes.Sort();
            var refreshedScopes = refreshedToken.Scopes.Split(' ').ToList();
            refreshedScopes.Sort();
            refreshedScopes.ShouldBe(originalScopes, "Scopes should not have been changed");
        }
    }
}