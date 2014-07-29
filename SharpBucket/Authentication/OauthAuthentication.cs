using System.Collections.Generic;
using System.Linq;

namespace SharpBucket.Authentication{
    public abstract class OauthAuthentication{
        protected readonly string ConsumerKey;
        protected readonly string ConsumerSecret;
        protected const string baseUrl = "https://bitbucket.org/api/1.0/";

        protected OauthAuthentication(string consumerKey, string consumerSecret){
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
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
    }
}