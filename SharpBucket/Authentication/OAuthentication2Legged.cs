using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication{
    public class OAuthentication2Legged : OauthAuthentication, IAuthenticate{
        private readonly RestClient client;

        public OAuthentication2Legged(string consumerKey, string consumerSecret, string baseUrl)
            : base(consumerKey, consumerSecret, baseUrl){
            client = new RestClient(baseUrl){
                Authenticator = OAuth1Authenticator.ForProtectedResource(ConsumerKey, ConsumerSecret, null, null)
            };
        }

        public T GetResponse<T>(string url, Method method, T body){
            var executeMethod = typeof (RequestExcecutor).GetMethod("ExectueRequest");
            var generic = executeMethod.MakeGenericMethod(typeof (T));
            return (T) generic.Invoke(this, new object[]{url, method, body, client});
        }
    }
}