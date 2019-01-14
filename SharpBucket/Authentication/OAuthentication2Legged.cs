using System;
using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class helps you authenticated with the BitBucket REST API via the 2 legged OAuth authentication.
    /// </summary>
    [Obsolete("Use OAuth1TwoLeggedAuthentication instead")]
    public class OAuthentication2Legged : OauthAuthentication
    {
        protected override IRestClient Client { get; }

        public OAuthentication2Legged(string consumerKey, string consumerSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl)
        {
            Client = new RestClient(baseUrl)
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, null, null)
            };
        }
    }
}