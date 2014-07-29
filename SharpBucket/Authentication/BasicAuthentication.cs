using System;
using System.Text;
using NServiceKit.Common.Web;
using NServiceKit.Text;

namespace SharpBucket.Authentication{
    public class BasicAuthentication : IAuthenticate{
        private readonly string credentialsToken;

        public BasicAuthentication(string username, string password){
            credentialsToken = GetCredentialsToken(username, password);
        }

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
        }
    }
}
