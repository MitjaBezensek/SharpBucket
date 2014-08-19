using RestSharp;

namespace SharpBucket.Authentication{
    public class BasicAuthentication : Authenticate{
        public BasicAuthentication(string username, string password, string baseUrl){
            client = new RestClient(baseUrl){
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}