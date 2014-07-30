using System;
using System.Net;
using NServiceKit.ServiceClient.Web;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using RestSharp;
using SharpBucket.Authentication;

namespace SharpBucket{
    public class SharpBucket{
        private IAuthenticate authenticator;
        protected string _baseUrl;

        public void BasicAuthentication(string username, string password, string baseUrl){
            authenticator = new BasicAuthentication(username, password, baseUrl);
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

        private T Send<T>(IReturn<T> request, Method method, string overrideUrl = null){
            using (new ConfigScope()){
                var relativeUrl = overrideUrl ?? request.ToUrl(method.ToString());
                string response;
                try{
                    response = authenticator.GetResponse(relativeUrl, method, request);
                }
                catch (WebException ex){
                    string errorBody = ex.GetResponseBody();
                    var errorStatus = ex.GetStatus() ?? HttpStatusCode.BadRequest;
                    if (ex.IsAny400()){
                        Console.WriteLine(errorBody);
                        Console.WriteLine(errorStatus);
                    }
                    response = null;
                }
                return response.FromJson<T>();
            }
        }

        public T Get<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, Method.GET, overrideUrl);
        }

        public T Post<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, Method.POST, overrideUrl);
        }

        public T Put<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, Method.PUT, overrideUrl);
        }

        public T Delete<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, Method.DELETE, overrideUrl);
        }
    }
}