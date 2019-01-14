using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/meta/authentication#oauth-2
    /// Client Credentials Grant (4.4)
    /// </summary>
    internal class OAuth2TokenProvider
    {
        private const string TokenUrl = "https://bitbucket.org/site/oauth2/access_token";

        public string ConsumerKey { get; }

        public string ConsumerSecret { get; }

        public OAuth2TokenProvider(string consumerKey, string consumerSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        public Token GetToken()
        {
            var tempClient = new RestClient(TokenUrl)
            {
                Authenticator = new HttpBasicAuthenticator(ConsumerKey, ConsumerSecret),
            };
            var request = new RestRequest
            {
                Method = Method.POST
            };
            request.AddParameter("grant_type", "client_credentials");
            var response = tempClient.Execute<Token>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unable to retrieve OAuth2 Token:{Environment.NewLine}{response.StatusCode}{Environment.NewLine}{response.Content}");
            }

            return response.Data;
        }

        public Token RefreshToken(Token token)
        {
            var tempClient = new RestClient(TokenUrl)
            {
                Authenticator = new HttpBasicAuthenticator(ConsumerKey, ConsumerSecret),
            };
            var request = new RestRequest
            {
                Method = Method.POST
            };
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", token.RefreshToken);
            var response = tempClient.Execute<Token>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unable to refresh OAuth2 Token:{Environment.NewLine}{response.StatusCode}{Environment.NewLine}{response.Content}");
            }

            return response.Data;
        }
    }
}
