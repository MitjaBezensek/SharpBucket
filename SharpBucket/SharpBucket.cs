using System.Net;
using NServiceKit.Common;
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

        public void OAuthAuthentication(string apiKey, string secretApiKey){
            authenticator = new OauthAuthentication(apiKey, secretApiKey);
        }

        private string Send(string relativeUrl, string method, string body){
            try{
                var url = BaseUrl.CombineWith(relativeUrl);

                var response = url.SendStringToUrl(method, body, requestFilter: req =>{
                    req.Accept = MimeTypes.Json;
                    authenticator.AuthenticateRequest(req);
                    if (method == HttpMethods.Post || method == HttpMethods.Put){
                        req.ContentType = "application/x-www-form-urlencoded";
                    }
                });

                return response;
            }
            catch (WebException ex){
                string errorBody = ex.GetResponseBody();
                var errorStatus = ex.GetStatus() ?? HttpStatusCode.BadRequest;

                if (ex.IsAny400()){
                }
                return null;
            }
        }

        private T Send<T>(IReturn<T> request, string method, bool sendRequestBody = true, string overrideUrl = null){
            var relativeUrl = overrideUrl ?? request.ToUrl(method);
            var body = sendRequestBody ? QueryStringSerializer.SerializeToString(request) : null;
            var json = Send(relativeUrl, method, body);
            var response = json.FromJson<T>();
            return response;
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
    }
}