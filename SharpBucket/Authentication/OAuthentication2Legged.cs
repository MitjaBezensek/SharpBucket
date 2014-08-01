using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication{
    public class OAuthentication2Legged : OauthAuthentication{
        public OAuthentication2Legged(string consumerKey, string consumerSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl){
            client = new RestClient(baseUrl){
                Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, null, null)
            };
        }
    }
}
