using RestSharp;

namespace SharpBucket.Authentication{
    public abstract class Authenticate{
        protected RestClient client;

        public virtual T GetResponse<T>(string url, Method method, T body){
            var executeMethod = typeof (RequestExecutor).GetMethod("ExecuteRequest");
            var generic = executeMethod.MakeGenericMethod(typeof (T));
            return (T) generic.Invoke(this, new object[]{url, method, body, client});
        }

        public virtual string GetResponse(string url, Method method)
        {
            var executeMethod = typeof(RequestExecutor).GetMethod("ExecuteRequest2");
            return (string)executeMethod.Invoke(this, new object[] { url, method, client });
        }
    }
}