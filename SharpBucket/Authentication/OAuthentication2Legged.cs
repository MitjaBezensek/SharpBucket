using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace SharpBucket.Authentication{
    public class OAuthentication2Legged : OauthAuthentication, IAuthenticate{
        public OAuthentication2Legged(string apiKey, string secretApiKey) : base(apiKey, secretApiKey){
        }

        public string GetResponse(string url, string method, string body){
            var oauthSession = CreateSession(_apiKey, _secretApiKey);
            return ExecuteRequest(url, method, body, oauthSession);
        }

        private OAuthSession CreateSession(string consumer, string consumerSecret){
            var consumerContext = new OAuthConsumerContext{
                ConsumerKey = consumer,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1,
                UseHeaderForOAuthParameters = true,
            };
            return new OAuthSession(consumerContext, requestUrl, userAuthorizeUrl, accessUrl);
        }
    }
}