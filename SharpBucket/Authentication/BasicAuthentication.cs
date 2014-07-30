using NServiceKit.ServiceHost;
using RestSharp;


namespace SharpBucket.Authentication{
    public class BasicAuthentication : IAuthenticate{
        private RestClient client;
        private const string baseUrl = "https://bitbucket.org/api/1.0/";

        public BasicAuthentication(string username, string password){
            client = new RestClient(baseUrl){Authenticator = new HttpBasicAuthenticator(username, password)};
        }

        public string GetResponse<T>(string url, string method, IReturn<T> body){
            return RequestExcecutor.ExectueRequest(url, method, body, client);
        }
    }
}