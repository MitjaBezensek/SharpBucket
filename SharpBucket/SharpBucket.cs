using System.Net;
using NServiceKit.Text;
using RestSharp;
using SharpBucket.Authentication;

namespace SharpBucket{
    public class SharpBucket{
        private IAuthenticate authenticator;
        protected string _baseUrl;

        public void BasicAuthentication(string username, string password){
            authenticator = new BasicAuthentication(username, password, _baseUrl);
        }

        public void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey){
            authenticator = new OAuthentication2Legged(consumerKey, consumerSecretKey, _baseUrl);
        }

        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string callback = "oob"){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, callback, _baseUrl);
            return (OAuthentication3Legged) authenticator;
        }

        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string oauthToken, string oauthTokenSecret){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, oauthToken, oauthTokenSecret, _baseUrl);
            return (OAuthentication3Legged) authenticator;
        }

        private T Send<T>(T request, Method method, string overrideUrl = null){
            var relativeUrl = overrideUrl;
            string response;
            try{
                response = authenticator.GetResponse(relativeUrl, method, request);
            }
            catch (WebException ex){               
                response = null;
            }
            return response.FromJson<T>();
        }

        public T Get<T>(T request, string overrideUrl){
            return Send(request, Method.GET, overrideUrl);
        }

        public T Post<T>(T request, string overrideUrl){
            return Send(request, Method.POST, overrideUrl);
        }

        public T Put<T>(T request, string overrideUrl){
            return Send(request, Method.PUT, overrideUrl);
        }

        public T Delete<T>(T request, string overrideUrl){
            return Send(request, Method.DELETE, overrideUrl);
        }
    }
}