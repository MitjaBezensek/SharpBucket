using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;

namespace SharpBucket.Authentication{
    public class OauthAuthentication : IAuthenticate{
        private readonly string _apiKey;
        private readonly string _secretApiKey;
        private const string requestUrl = " https://bitbucket.org/api/1.0/oauth/request_token";
        private const string userAuthorizeUrl = "https://bitbucket.org/api/1.0/oauth/authenticate";
        private const string accessUrl = "https://bitbucket.org/api/1.0/oauth/access_token";

        public OauthAuthentication(string apiKey, string secretApiKey){
            _apiKey = apiKey;
            _secretApiKey = secretApiKey;
        }

        public string GetResponse(string url, string method, string body){
            var oauthSession = CreateSession(_apiKey, _secretApiKey);
            var dictionary = GetParameterDictionary(body);
            HttpWebResponse results = null;
            switch (method){
                case "PUT":
                    oauthSession.WithFormParameters((IDictionary) dictionary);
                    results = oauthSession.Request().Put().ForUrl(url).ToWebResponse();
                    break;
                case "POST":
                    oauthSession.WithFormParameters((IDictionary) dictionary);
                    results = oauthSession.Request().Post().ForUrl(url).ToWebResponse();
                    break;
                case "GET":
                    results = oauthSession.Request().Get().ForUrl(url).ToWebResponse();
                    break;
                case "DELETE":
                    results = oauthSession.Request().Delete().ForUrl(url).ToWebResponse();
                    break;
            }
            var reader = new StreamReader(results.GetResponseStream(), new UTF8Encoding());
            return reader.ReadToEnd();
        }

        private static Dictionary<string, string> GetParameterDictionary(string body){
            var dictionary = new Dictionary<string, string>();
            if (body != null){
                var parameters = body.Split('&');
                foreach (var keyValue in parameters.Select(parameter => parameter.Split('='))){
                    dictionary.Add(keyValue[0], keyValue[1]);
                }
            }
            return dictionary;
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
