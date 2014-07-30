using System;
using System.Text;
using NServiceKit.Common.Web;
<<<<<<< HEAD
using NServiceKit.Text;
=======
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using RestSharp;
>>>>>>> Oauth3Legged

namespace SharpBucket.Authentication{
    public class BasicAuthentication : IAuthenticate{
        private RestClient client;
        private const string baseUrl = "https://bitbucket.org/api/1.0/";

        public BasicAuthentication(string username, string password){
            client = new RestClient(baseUrl){Authenticator = new HttpBasicAuthenticator(username, password)};
        }

<<<<<<< HEAD
        private string GetCredentialsToken(string username, string paswword){
            var usernameAndPassword = string.Format("{0}:{1}", username, paswword);
            var encodedUsernameAndPassword = Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernameAndPassword));
            return string.Format("Basic {0}", encodedUsernameAndPassword);
        }

        public string GetResponse(string url, string method, string body){
            var response = url.SendStringToUrl(method, body, requestFilter: req =>{
                req.Accept = MimeTypes.Json;
                req.Headers.Add("Authorization", credentialsToken);
                if (method == HttpMethods.Post || method == HttpMethods.Put){
                    req.ContentType = "application/x-www-form-urlencoded";
                }
            });
            return response;
=======
        public string GetResponse<T>(string url, string method, IReturn<T> body){
           return RequestExcecutor.ExectueRequest(url, method, body, client);
>>>>>>> Oauth3Legged
        }
    }
}
