using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    public class OAuthentication2 : OauthAuthentication
    {
        private const string TokenUrl = "https://bitbucket.org/site/oauth2/access_token";
        private const string TokenType = "Bearer";
        private string _token;

        public OAuthentication2(string consumerKey, string consumerSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl)
        {
        }

        public void GetToken()
        {
            var tempClient = new RestClient(TokenUrl)
            {
                Authenticator = new HttpBasicAuthenticator(ConsumerKey, ConsumerSecret),
            };
            var request = new RestRequest
            {
                Method = Method.POST
            };
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("grant_type", "client_credentials");
            var response = tempClient.Execute<Token>(request);
            _token = response.Data.AccessToken;
            client = new RestClient(_baseUrl)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(_token, TokenType)
            };
        }
    }
}