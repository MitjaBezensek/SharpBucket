using System;
using System.Diagnostics;
using NServiceKit.ServiceHost;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;

namespace SharpBucket.Authentication{
    public class OAuthentication3Legged : OauthAuthentication, IAuthenticate{
        private readonly string OAuthToken;
        private readonly string OauthTokenSecret;
        private RestClient client;
        private const string requestUrl = "oauth/request_token";
        private const string userAuthorizeUrl = "oauth/authenticate";
        private const string accessUrl = "access_token";

        public OAuthentication3Legged(string consumerKey, string consumerSecret) : base(consumerKey, consumerSecret){
            // get the tokens
            var authClient = new RestClient(baseUrl){Authenticator = OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret, "oob")};
            var request = new RestRequest(requestUrl, Method.POST);
            var response = authClient.Execute(request);
    
            var qs = HttpUtility.ParseQueryString(response.Content);
            var oauth_token = qs["oauth_token"];
            var oauth_token_secret = qs["oauth_token_secret"];
            request = new RestRequest(userAuthorizeUrl);
            request.AddParameter("oauth_token", oauth_token);
            var url = authClient.BuildUri(request).ToString();
            Process.Start(url);
            var verifier = Console.ReadLine();
            request = new RestRequest(accessUrl, Method.POST);

            authClient.Authenticator = OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, oauth_token, oauth_token_secret, verifier);
            response = authClient.Execute(request);
            qs = HttpUtility.ParseQueryString(response.Content);
            OAuthToken = qs["oauth_token"];
            OauthTokenSecret = qs["oauth_token_secret"];
        }

        public string GetResponse<T>(string url, string method, IReturn<T> body){
            if (client == null){
                client = new RestClient(baseUrl){
                    Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, OAuthToken, OauthTokenSecret)
                };
            }
            return RequestExcecutor.ExectueRequest(url, method, body, client);
        }
    }
}