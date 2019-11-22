using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class helps you authenticate with the BitBucket REST API via the 2 legged OAuth authentication.
    /// </summary>
    public sealed class OAuth1TwoLeggedAuthentication : Authenticate
    {
        public OAuth1TwoLeggedAuthentication(string consumerKey, string consumerSecret, string baseUrl)
        {
            Client = new RestClient(baseUrl)
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(consumerKey, consumerSecret, null, null)
            };
        }
    }
}
