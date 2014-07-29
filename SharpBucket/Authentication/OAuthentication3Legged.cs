using System;
using System.Diagnostics;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace SharpBucket.Authentication{
    public class OAuthentication3Legged : OauthAuthentication, IAuthenticate{
        private IToken accessToken;
        private OAuthConsumerContext context;
        private IToken requestToken;
       
        public OAuthentication3Legged(string apiKey, string secretApiKey) : base(apiKey, secretApiKey){
            Authenticate();
        }

        public string GetResponse(string url, string method, string body){
            var authorizationContext = new OAuthConsumerContext { ConsumerKey = _apiKey, ConsumerSecret = _secretApiKey, SignatureMethod = SignatureMethod.HmacSha1};
            var oauthSession = new OAuthSession(authorizationContext, requestUrl, userAuthorizeUrl, accessUrl);
            return ExecuteRequest(url, method, body, oauthSession);
        }

        private void Authenticate(){
            context = new OAuthConsumerContext { ConsumerKey = _apiKey, ConsumerSecret = _secretApiKey, SignatureMethod = SignatureMethod.HmacSha1 };
            var session = new OAuthSession(context, requestUrl, userAuthorizeUrl, accessUrl);
            requestToken = session.GetRequestToken();
            var link = session.GetUserAuthorizationUrlForToken(requestToken);
            Process.Start(link);
            Console.WriteLine("Please enter the PIN:");
            var pin = Console.ReadLine();
            accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, pin);
            accessToken.ConsumerKey = _apiKey;
            //accessToken.Token = requestToken.Token;
            //accessToken.TokenSecret = requestToken.TokenSecret;
            accessToken.Realm = requestToken.Realm;
           
        }
    }
}