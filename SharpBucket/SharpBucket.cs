using System;
using System.Net;
using NServiceKit.Common.Web;
using NServiceKit.ServiceClient.Web;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using SharpBucket.Authentication;

namespace SharpBucket{
    public class SharpBucket{
        protected string BaseUrl { private get; set; }
        private IAuthenticate authenticator;

        public void BasicAuthentication(string username, string password){
            authenticator = new BasicAuthentication(username, password);
        }

        public void OAuth2LeggedAuthentication(string apiKey, string secretApiKey){
            authenticator = new OAuthentication2Legged(apiKey, secretApiKey);
        }

        public OAuthentication3Legged OAuth3LeggedAuthentication(string apiKey, string secretApiKey, string callback = "oob"){
            authenticator = new OAuthentication3Legged(apiKey, secretApiKey, callback);
            return (OAuthentication3Legged) authenticator;
        }


        private T Send<T>(IReturn<T> request, string method, bool sendRequestBody = true, string overrideUrl = null){
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
                    }
                    ret = null;
                }
                var json = ret;
                var response = json.FromJson<T>();
                return response;
            }
        }

        public T Get<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Get, false, overrideUrl);
        }

        public T Post<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Post, overrideUrl: overrideUrl);
        }

        public T Put<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Put, overrideUrl: overrideUrl);
        }

        public T Delete<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Delete, false, overrideUrl);
        }

        public class ConfigScope : IDisposable{
            private readonly JsConfigScope jsConfigScope;

            public ConfigScope(){
                jsConfigScope = JsConfig.With(
                    emitLowercaseUnderscoreNames: true,
                    emitCamelCaseNames: false);
            }

            public void Dispose(){
                jsConfigScope.Dispose();
            }
        }
    }
}