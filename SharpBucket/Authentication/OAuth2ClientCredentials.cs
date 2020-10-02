using System;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class helps you authenticate with the Bitbucket REST API via the OAuth2 client credentials authentication.
    /// </summary>
    public sealed class OAuth2ClientCredentials : Authenticate
    {
        private const string TokenType = "Bearer";
        private const int RefreshMargin = 5;

        private string BaseUrl { get; }
        private OAuth2TokenProvider TokenProvider { get; }
        private OAuth2Token Token { get; set; }
        private DateTime TokenExpirationDate { get; set; }

        protected override IRestClient Client
        {
            get
            {
                if (TokenExpirationDate <= DateTime.UtcNow)
                {
                    base.Client = RefreshClient();
                }

                return base.Client;
            }
            set => base.Client = value;
        }

        public OAuth2ClientCredentials(string consumerKey, string consumerSecret, string baseUrl)
        {
            TokenProvider = new OAuth2TokenProvider(consumerKey, consumerSecret);
            BaseUrl = baseUrl;
            Client = CreateClient();
        }

        private IRestClient CreateClient()
        {
            var token = TokenProvider.GetClientCredentialsToken();
            return this.CreateClient(token);
        }

        private IRestClient RefreshClient()
        {
            var newToken = TokenProvider.RefreshToken(this.Token);
            return this.CreateClient(newToken);
        }

        private IRestClient CreateClient(OAuth2Token token)
        {
            this.Token = token;
            this.TokenExpirationDate = token.ExpiresAt.AddSeconds(0 - RefreshMargin);

            var accessToken = this.Token.AccessToken;
            return new RestClient(BaseUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, TokenType)
            };
        }
    }
}
