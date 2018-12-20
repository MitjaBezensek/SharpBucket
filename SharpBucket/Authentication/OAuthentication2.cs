using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    public class OAuthentication2 : OauthAuthentication
    {
        private const string TokenType = "Bearer";
        private string _accessToken;

        public OAuthentication2(string consumerKey, string consumerSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl)
        {
        }

        public void GetToken()
        {
            // TODO the token (and not just the access token) should be kept somewhere to implement refresh token scenario one day
            var tokenProvider = new OAuth2TokenProvider(ConsumerKey, ConsumerSecret);
            _accessToken = tokenProvider.GetToken().AccessToken;
            client = new RestClient(_baseUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_accessToken, TokenType)
            };
        }
    }
}