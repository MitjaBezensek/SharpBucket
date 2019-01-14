using System;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class helps you authenticate with the BitBucket REST API via the OAuth2 client credentials authentication, including support of 
    /// </summary>
    public class OAuth2ClientCredentials : Authenticate
    {
        private const string TokenType = "Bearer";
        private const int RefreshMargin = 5;

        private string BaseUrl { get; }
        private OAuth2TokenProvider TokenProvider { get; }
        private Token Token { get; set; }
        private DateTime TokenExpirationDate { get; set; }

        private IRestClient _client;
        protected override IRestClient Client
        {
            get
            {
                if (TokenExpirationDate <= DateTime.UtcNow)
                {
                    _client = RefreshClient();
                }

                return _client;
            }
        }

        public OAuth2ClientCredentials(string consumerKey, string consumerSecret, string baseUrl)
        {
            TokenProvider = new OAuth2TokenProvider(consumerKey, consumerSecret);
            BaseUrl = baseUrl;
            _client = CreateClient();
        }

        private IRestClient CreateClient()
        {
            var token = TokenProvider.GetToken();
            return this.CreateClient(token);
        }

        private IRestClient RefreshClient()
        {
            var newToken = TokenProvider.RefreshToken(this.Token);
            return this.CreateClient(newToken);
        }

        private IRestClient CreateClient(Token token)
        {
            this.Token = token;
            this.TokenExpirationDate = DateTime.UtcNow.AddSeconds(token.ExpiresIn - RefreshMargin);

            var accessToken = this.Token.AccessToken;
            return new RestClient(BaseUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(accessToken, TokenType)
            };
        }
    }
}
