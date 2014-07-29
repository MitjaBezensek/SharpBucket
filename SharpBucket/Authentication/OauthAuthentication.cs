using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DevDefined.OAuth.Consumer;

namespace SharpBucket.Authentication{
    public abstract class OauthAuthentication{
        protected readonly string _apiKey;
        protected readonly string _secretApiKey;
        protected const string requestUrl = "https://bitbucket.org/api/1.0/oauth/request_token";
        protected const string userAuthorizeUrl = "https://bitbucket.org/api/1.0/oauth/authenticate";
        protected const string accessUrl = "https://bitbucket.org/api/1.0/oauth/access_token";

        protected OauthAuthentication(string apiKey, string secretApiKey){
            _apiKey = apiKey;
            _secretApiKey = secretApiKey;
        }

        protected static Dictionary<string, string> GetParameterDictionary(string body){
            var dictionary = new Dictionary<string, string>();
            if (body != null){
                var parameters = body.Split('&');
                foreach (var keyValue in parameters.Select(parameter => parameter.Split('='))){
                    dictionary.Add(keyValue[0], keyValue[1]);
                }
            }
            return dictionary;
        }

        protected static string ExecuteRequest(string url, string method, string body, OAuthSession oauthSession){
            HttpWebResponse results = null;
            var dictionary = GetParameterDictionary(body);
            switch (method){
                case "PUT":
                    oauthSession.Request().Context.RawContentType = "application/x-www-form-urlencoded";
                    oauthSession.WithHeaders((IDictionary) dictionary);
                    results = oauthSession.Request().Put().ForUrl(url).ToWebResponse();
                    break;
                case "POST":
                    oauthSession.Request().Context.RawContentType = "application/x-www-form-urlencoded";
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
    }
}