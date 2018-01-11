using System.Collections.Generic;
using RestSharp;

namespace SharpBucket.Authentication
{
    public abstract class Authenticate
    {
        protected RestClient client;

        public virtual T GetResponse<T>(string url, Method method, T body, IDictionary<string, object> requestParameters)
        {
            var executeMethod = typeof(RequestExecutor).GetMethod("ExecuteRequest");
            var generic = executeMethod.MakeGenericMethod(typeof(T));
            return (T)generic.Invoke(this, new object[] { url, method, body, client, requestParameters });
        }
    }
}