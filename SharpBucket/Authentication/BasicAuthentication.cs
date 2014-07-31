using System.Reflection;
using RestSharp;

namespace SharpBucket.Authentication{
    public class BasicAuthentication : IAuthenticate{
        private readonly RestClient client;

        public BasicAuthentication(string username, string password, string baseUrl){
            client = new RestClient(baseUrl){
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }

        public T GetResponse<T>(string url, Method method, T body){
            MethodInfo executeMethod = typeof (RequestExcecutor).GetMethod("ExectueRequest");
            MethodInfo generic = executeMethod.MakeGenericMethod(typeof (T));
            return (T) generic.Invoke(this, new object[]{url, method, body, client});
        }
    }
}