using System;
using System.Net;
using System.Text;

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

        public void AuthenticateRequest(HttpWebRequest req){
            req.Headers.Add("Authorization", credentialsToken);
        }
    }
}