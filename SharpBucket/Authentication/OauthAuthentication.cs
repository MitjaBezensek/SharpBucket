using System.Net;
using SharpBucket.Authentication.OAuth;

namespace SharpBucket.Authentication{
    public class OauthAuthentication : IAuthenticate{
        private readonly string _apiKey;
        private readonly string _secretApiKey;

        public OauthAuthentication(string apiKey, string secretApiKey){
            _apiKey = apiKey;
            _secretApiKey = secretApiKey;
        }

        public void AuthenticateRequest(HttpWebRequest req){
            var oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();
            string normalizedUrl;
            string normalizedRequestParameters;
            var sig = oAuth.GenerateSignature(
                req.RequestUri,
                _apiKey,
                _secretApiKey,
                null,
                null,
                req.Method,
                timeStamp,
                nonce,
                out normalizedUrl,
                out normalizedRequestParameters);
            var normalizedParametersFull = normalizedRequestParameters + "&oauth_signature=" + sig;
            var headerValue = "OAuth " + normalizedParametersFull.Replace("&", ", ");
            req.Headers.Add("Authorization", headerValue);
        }
    }
}
