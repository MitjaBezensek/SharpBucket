using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class helps you authenticate with the BitBucket REST API via the 3 legged OAuth authentication.
    /// </summary>
    public class OAuth1ThreeLeggedAuthentication : Authenticate
    {
        private const string RequestUrl = "oauth/request_token";
        private const string UserAuthorizeUrl = "oauth/authenticate";
        private const string AccessUrl = "oauth/access_token";

        private string ConsumerKey { get; }
        private string ConsumerSecret { get; }
        private string Callback { get; }
        private string BaseUrl { get; }

        private string OAuthToken { get; set; }
        private string OAuthTokenSecret { get; set; }

        private IRestClient _client;
        protected override IRestClient Client
        {
            get
            {
                if (_client == null)
                {
                    if (OAuthToken == null)
                    {
                        throw new InvalidOperationException("StartAuthentication and AuthenticateWithPin must be called before being able to do any request with this authentication mode");
                    }
                    _client = new RestClient(BaseUrl)
                    {
                        Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, OAuthToken, OAuthTokenSecret)
                    };
                }

                return _client;
            }
        }

        private OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecret, string baseUrl)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            BaseUrl = baseUrl;
        }

        public OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecret, string callback, string baseUrl)
            : this(consumerKey, consumerSecret, baseUrl)
        {
            this.Callback = callback;
        }

        public OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecret, string oauthToken, string oauthTokenSecret, string baseUrl)
            : this(consumerKey, consumerSecret, baseUrl)
        {
            OAuthToken = oauthToken;
            OAuthTokenSecret = oauthTokenSecret;
        }

        /// <summary>
        /// Sets the authentication tokens.
        /// </summary>
        /// <exception cref="System.Net.WebException">REST client encountered an error:  + response.ErrorMessage</exception>
        private void SetAuthTokens(IRestClient client, string method)
        {
            var request = new RestRequest(method, Method.POST);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new WebException("REST client encountered an error: " + response.ErrorMessage, response.ErrorException);
            }

            var qs = HttpUtility.ParseQueryString(response.Content);
            OAuthToken = qs["oauth_token"];
            OAuthTokenSecret = qs["oauth_token_secret"];
        }

        /// <summary>
        /// Start the OAuth authentication process.
        /// The method returns the the URL where the user can authorize your application to act on his/her behalf.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket#OAuthonBitbucket-Step3.RedirecttheusertoBitbuckettoauthorizeyourapplication
        /// </summary>
        public string StartAuthentication()
        {
            var restClient = new RestClient(BaseUrl)
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, Callback)
            };

            SetAuthTokens(restClient, RequestUrl);

            Contract.Assert(
                !String.IsNullOrWhiteSpace(OAuthToken) &&
                !String.IsNullOrWhiteSpace(OAuthTokenSecret));

            var request = new RestRequest(UserAuthorizeUrl);
            request.AddParameter("oauth_token", OAuthToken);

            return restClient.BuildUri(request).ToString();
        }

        /// <summary>
        /// The method is used to obtain the credentials that let you access resources on BitBucket.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket#OAuthonBitbucket-Step4.RequestanAccessToken
        /// </summary>
        /// <param name="pin">The pin / verifier that was obtained in the previous step.</param>
        public void AuthenticateWithPin(string pin)
        {
            var restClient = new RestClient(BaseUrl)
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(ConsumerKey, ConsumerSecret, OAuthToken, OAuthTokenSecret, pin)
            };

            SetAuthTokens(restClient, AccessUrl);

            Contract.Assert(
                !String.IsNullOrWhiteSpace(OAuthToken) &&
                !String.IsNullOrWhiteSpace(OAuthTokenSecret));
        }
    }
}
