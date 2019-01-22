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

        private OAuth1Token RequestToken { get; set; }
        private OAuth1Token AccessToken { get; set; }

        private IRestClient _client;
        protected override IRestClient Client
        {
            get
            {
                if (_client == null)
                {
                    if (AccessToken == null)
                    {
                        throw new InvalidOperationException("StartAuthentication and AuthenticateWithPin must be called before being able to do any request with this authentication mode");
                    }
                    _client = new RestClient(BaseUrl)
                    {
                        Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, AccessToken.Token, AccessToken.Secret)
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

        /// <summary>
        /// Initialize a new instance of <see cref="OAuth1ThreeLeggedAuthentication"/>
        /// to perform the whole Oauth1 3 legged process.
        /// </summary>
        /// <para>
        /// The <see cref="StartAuthentication"/> and <see cref="AuthenticateWithPin"/> methods
        /// will need to be called to complete the authentication process before being able to do any request.
        /// </para>
        public OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecret, string callback, string baseUrl)
            : this(consumerKey, consumerSecret, baseUrl)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="OAuth1ThreeLeggedAuthentication"/>
        /// to perform requests in an Oauth1 3 legged session which is already opened.
        /// </summary>
        public OAuth1ThreeLeggedAuthentication(string consumerKey, string consumerSecret, string oauthToken, string oauthTokenSecret, string baseUrl)
            : this(consumerKey, consumerSecret, baseUrl)
        {
            this.AccessToken = new OAuth1Token(oauthToken, oauthTokenSecret);
        }

        /// <summary>
        /// Gets the authentication token and secret.
        /// </summary>
        /// <exception cref="System.Net.WebException">REST client encountered an error:  + response.ErrorMessage</exception>
        private OAuth1Token GetAuthToken(IRestClient client, string method)
        {
            var request = new RestRequest(method, Method.POST);
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                throw new WebException("REST client encountered an error: " + response.ErrorMessage, response.ErrorException);
            }

            var qs = HttpUtility.ParseQueryString(response.Content);
            return new OAuth1Token(qs["oauth_token"], qs["oauth_token_secret"]);
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

            this.RequestToken = GetAuthToken(restClient, RequestUrl);

            var request = new RestRequest(UserAuthorizeUrl);
            request.AddParameter("oauth_token", this.RequestToken.Token);

            return restClient.BuildUri(request).ToString();
        }

        /// <summary>
        /// The method is used to obtain the credentials that let you access resources on BitBucket.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/OAuth+on+Bitbucket#OAuthonBitbucket-Step4.RequestanAccessToken
        /// </summary>
        /// <param name="pin">The pin / verifier that was obtained in the previous step.</param>
        /// <returns>The access token that will be used for further requests in that session,
        /// and that you may use in another session to skip a part of the 3 legged process if you keep it somewhere.</returns>
        public OAuth1Token AuthenticateWithPin(string pin)
        {
            if (RequestToken == null) throw new InvalidOperationException("StartAuthentication must be called before");

            var restClient = new RestClient(BaseUrl)
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(ConsumerKey, ConsumerSecret, RequestToken.Token, RequestToken.Secret, pin)
            };

            AccessToken = GetAuthToken(restClient, AccessUrl);

            return AccessToken;
        }
    }
}
