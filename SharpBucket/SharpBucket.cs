using System;
using System.Net;
using NServiceKit.Common.Web;
using NServiceKit.ServiceClient.Web;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using SharpBucket.Authentication;

namespace SharpBucket{
    public class SharpBucket{
        private IAuthenticate authenticator;

        public void BasicAuthentication(string username, string password){
            authenticator = new BasicAuthentication(username, password);
        }

        public void OAuth2LeggedAuthentication(string consumerKey, string consumerSecretKey){
            authenticator = new OAuthentication2Legged(consumerKey, consumerSecretKey);
        }

        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string callback = "oob"){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, callback);
            return (OAuthentication3Legged) authenticator;
        }

        public OAuthentication3Legged OAuth3LeggedAuthentication(string consumerKey, string consumerSecretKey, string oauthToken, string oauthTokenSecret){
            authenticator = new OAuthentication3Legged(consumerKey, consumerSecretKey, oauthToken, oauthTokenSecret);
            return (OAuthentication3Legged) authenticator;
        }


        private T Send<T>(IReturn<T> request, string method, string overrideUrl = null){
            using (new ConfigScope()){
                var relativeUrl = overrideUrl ?? request.ToUrl(method);
                string ret;
                try{
                    ret = authenticator.GetResponse(relativeUrl, method, request);
                }
                catch (WebException ex){
                    string errorBody = ex.GetResponseBody();
                    var errorStatus = ex.GetStatus() ?? HttpStatusCode.BadRequest;
                    if (ex.IsAny400()){
                        Console.WriteLine(errorBody);
                        Console.WriteLine(errorStatus);
                    }
                    ret = null;
                }
                var json = ret;
                var response = json.FromJson<T>();
                return response;
            }
        }

        public T Get<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Get, overrideUrl);
        }

        public T Post<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Post, overrideUrl);
        }

        public T Put<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Put, overrideUrl);
        }

        public T Delete<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Delete, overrideUrl);
        }
    }
}