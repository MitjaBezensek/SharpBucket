using System;
using System.Net;
using NServiceKit.Common;
using NServiceKit.Common.Web;
using NServiceKit.ServiceClient.Web;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using SharpBucket.Authentication;

namespace SharpBucket{
    public class SharpBucket{
        private string BaseUrl { get; set; }
        private IAuthenticate authenticator;

        public void BasicAuthentication(string username, string password){
            authenticator = new BasicAuthentication(username, password);
        }

        private string Send(string relativeUrl, string method, string body){
            try{
                var url = BaseUrl.CombineWith(relativeUrl);

                var response = url.SendStringToUrl(method: method, requestBody: body, requestFilter: req =>{
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

        public T Send<T>(IReturn<T> request){
            if (authenticator == null){
                Console.WriteLine("Need to authenticate before first use!");
            }
            var method = request is IPost<T> ?
                HttpMethods.Post
                : request is IPut<T> ?
                    HttpMethods.Put
                    : request is IDelete<T> ?
                        HttpMethods.Delete :
                        HttpMethods.Get;

            return Send(request, method, sendRequestBody: false);
        }

        public T Get<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Get, sendRequestBody: false, overrideUrl: overrideUrl);
        }

        public T Post<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Post, overrideUrl: overrideUrl);
        }

        public T Put<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Put, overrideUrl: overrideUrl);
        }

        public T Delete<T>(IReturn<T> request, string overrideUrl = null){
            return Send(request, HttpMethods.Delete, sendRequestBody: false, overrideUrl: overrideUrl);
        }
    }
}