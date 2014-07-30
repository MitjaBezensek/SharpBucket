using System;
using System.Net;
<<<<<<< HEAD
using NServiceKit.Common;
=======
>>>>>>> Oauth3Legged
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

        public void OAuthAuthentication(string apiKey, string secretApiKey, bool threeLegged = true){
            if (threeLegged){
                authenticator = new OAuthentication3Legged(apiKey, secretApiKey);
            }
            else{
                authenticator = new OAuthentication2Legged(apiKey, secretApiKey);
            }
        }


        private T Send<T>(IReturn<T> request, string method, bool sendRequestBody = true, string overrideUrl = null){
            using (new ConfigScope()){
                var relativeUrl = overrideUrl ?? request.ToUrl(method);
                string ret;
                try{
<<<<<<< HEAD
                    var body = sendRequestBody ? QueryStringSerializer.SerializeToString(request) : null;
                    var url = BaseUrl.CombineWith(relativeUrl);
                    ret = authenticator.GetResponse(url, method, body);
=======
                    ret = authenticator.GetResponse(relativeUrl, method, request);
>>>>>>> Oauth3Legged
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

<<<<<<< HEAD
        private class ConfigScope : IDisposable{
=======
        public class ConfigScope : IDisposable{
>>>>>>> Oauth3Legged
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
