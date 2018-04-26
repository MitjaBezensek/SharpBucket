using System.Collections.Generic;
using RestSharp;
using Serilog;

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

        /// <summary>
        /// With SeriLog logging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logger"></param>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="body"></param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        public virtual T GetResponse<T>(ILogger logger, string url, Method method, T body, IDictionary<string, object> requestParameters)
        {
            var executeMethod = typeof(RequestExecutor).GetMethod("ExecuteRequestWithLogging");
            var generic = executeMethod?.MakeGenericMethod(typeof(T));
            return (T)generic?.Invoke(this, new object[] { logger,url, method, body, client, requestParameters
            });
        }
    }
}