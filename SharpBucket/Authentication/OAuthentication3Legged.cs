using NServiceKit.ServiceHost;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;

namespace SharpBucket.Authentication{
    public class OAuthentication3Legged : OauthAuthentication, IAuthenticate{
        private string OAuthToken;
        private string OauthTokenSecret;
        private RestClient client;
        private const string requestUrl = "oauth/request_token";
        private const string userAuthorizeUrl = "oauth/authenticate";
        private const string accessUrl = "oauth/access_token";
        private readonly string callback;

        public OAuthentication3Legged(string consumerKey, string consumerSecret, string callback, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl){
            this.callback = callback;
        }

        public OAuthentication3Legged(string consumerKey, string consumerSecret, string oAuthToken, string oauthTokenSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl){
            OAuthToken = oAuthToken;
            OauthTokenSecret = oauthTokenSecret;
        }

        public string GetResponse<T>(string url, Method method, IReturn<T> body){
            if (client == null){
                client = new RestClient(_baseUrl){
                    Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, OAuthToken, OauthTokenSecret)
                };
            }
            return RequestExcecutor.ExectueRequest(url, method, body, client);
        }

        public string StartAuthentication(){
            var restClient = new RestClient(_baseUrl){Authenticator = OAuth1Authenticator.ForRequestToken(ConsumerKey, ConsumerSecret, callback)};
            var request = new RestRequest(requestUrl, Method.POST);
            var response = restClient.Execute(request);

            var qs = HttpUtility.ParseQueryString(response.Content);
            OAuthToken = qs["oauth_token"];
            OauthTokenSecret = qs["oauth_token_secret"];
            request = new RestRequest(userAuthorizeUrl);
            request.AddParameter("oauth_token", OAuthToken);
            return restClient.BuildUri(request).ToString();
        }

        public void AuthenticateWithPin(string pin){
            var request = new RestRequest(accessUrl, Method.POST);
            var restClient = new RestClient(_baseUrl){
                Authenticator = OAuth1Authenticator.ForAccessToken(ConsumerKey, ConsumerSecret, OAuthToken, OauthTokenSecret, pin)
            };
            var response = restClient.Execute(request);
            var qs = HttpUtility.ParseQueryString(response.Content);
            OAuthToken = qs["oauth_token"];
            OauthTokenSecret = qs["oauth_token_secret"];
        }
    }
}