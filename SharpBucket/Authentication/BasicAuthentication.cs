using RestSharp;

namespace SharpBucket.Authentication{
    public class BasicAuthentication : IAuthenticate{
        private readonly RestClient client;

        public BasicAuthentication(string username, string password, string baseUrl){
            client = new RestClient(baseUrl){
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public string GetResponse<T>(string url, Method method, T body){
            return RequestExcecutor.ExectueRequest(url, method, body, client);
        }
    }
}