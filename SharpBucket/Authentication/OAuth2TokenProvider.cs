using System;
using System.Net;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// Class that act like a client of the /site/oauth2/access_token route
    /// https://developer.atlassian.com/bitbucket/api/2/reference/meta/authentication#oauth-2
    /// </summary>
    public class OAuth2TokenProvider
    {
        private const string TokenUrl = "https://bitbucket.org/site/oauth2/access_token";

        /// <summary>
        /// Gets the the client id of the OAuth consumer that will be use to claim OAuth2 tokens.
        /// </summary>
        public string ConsumerKey { get; }

        /// <summary>
        /// Gets the the client secret of the OAuth consumer that will be use to claim OAuth2 tokens.
        /// </summary>
        public string ConsumerSecret { get; }

        /// <summary>
        /// Initialize a new instance of <see cref="OAuth2TokenProvider"/> class.
        /// </summary>
        /// <param name="consumerKey">The client id of an OAuth consumer.</param>
        /// <param name="consumerSecret">The client secret of an OAuth consumer.</param>
        /// <exception cref="NullReferenceException">If <paramref name="consumerKey"/> or <paramref name="consumerSecret"/> is null.</exception>
        public OAuth2TokenProvider(string consumerKey, string consumerSecret)
        {
            this.ConsumerKey = consumerKey ?? throw new NullReferenceException(nameof(consumerKey));
            this.ConsumerSecret = consumerSecret ?? throw new NullReferenceException(nameof(consumerSecret));
        }

        /// <summary>
        /// Authorization Code Grant (4.1)
        /// </summary>
        /// <param name="code">The code retrieved from the Authorize callback</param>
        /// <returns>
        /// An OAuth2 Token that allow to be authenticated as the user that give you that code,
        /// and with the scopes that correspond to your consumer key and secret.
        /// </returns>
        public OAuth2Token GetAuthorizationCodeToken(string code)
        {
            return GetToken("authorization_code", Parameter("code", code));
        }

        /// <summary>
        /// Resource Owner Password Credentials Grant (4.3)
        /// </summary>
        /// <param name="username">The username (email) of a user</param>
        /// <param name="password">The password of that user</param>
        /// <returns>
        /// An OAuth2 Token that allow to be authenticated as the user that give you its credentials,
        /// and with the scopes that correspond to your consumer key and secret.
        /// </returns>
        public OAuth2Token GetResourceOwnerPasswordCredentialsToken(string username, string password)
        {
            return GetToken("password", Parameter("username", username), Parameter("password", password));
        }

        /// <summary>
        /// Client Credentials Grant (4.4)
        /// </summary>
        /// <returns>
        /// An OAuth2 Token that allow to be authenticated as the user that correspond to your consumer key and secret.
        /// </returns>
        public OAuth2Token GetClientCredentialsToken()
        {
            return GetToken("client_credentials");
        }

        /// <summary>
        /// Ask to refresh a token
        /// </summary>
        /// <param name="token">The token to refresh</param>
        /// <returns>A refreshed OAuth2 Token equivalent to the given one.</returns>
        public OAuth2Token RefreshToken(OAuth2Token token)
        {
            return GetToken("refresh_token", Parameter("refresh_token", token.RefreshToken));
        }

        private OAuth2Token GetToken(string grantType, params Parameter[] parameters)
        {
            var tempClient = new RestClient(TokenUrl)
            {
                Authenticator = new HttpBasicAuthenticator(ConsumerKey, ConsumerSecret),
            };
            var request = new RestRequest
            {
                Method = Method.POST
            };
            request.AddParameter("grant_type", grantType);
            foreach (var parameter in parameters)
            {
                parameter.Type = ParameterType.GetOrPost;
                request.AddParameter(parameter);
            }
            var response = tempClient.Execute<OAuth2Token>(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Unable to retrieve OAuth2 Token:{Environment.NewLine}{response.StatusCode}{Environment.NewLine}{response.Content}");
            }

            return response.Data;
        }

        private static Parameter Parameter(string name, string value)
        {
            return new Parameter
            {
                Name = name,
                Value = value,
                Type = ParameterType.GetOrPost
            };
        }
    }
}
