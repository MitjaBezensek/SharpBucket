using System;
using System.Collections.Generic;
using RestSharp;
using SharpBucket.V2;

namespace SharpBucket.Authentication
{
    public abstract class Authenticate
    {
        protected abstract IRestClient Client { get; }

        internal RequestExecutor RequestExecutor { get; set; } = new RequestExecutorV2(); // Use V2 by default since V1 should disappear soon

        public virtual string GetResponse(string url, Method method, object body, IDictionary<string, object> requestParameters)
        {
            return RequestExecutor.ExecuteRequest(url, method, body, Client, requestParameters);
        }

        public virtual T GetResponse<T>(string url, Method method, object body, IDictionary<string, object> requestParameters)
            where T : new()
        {
            return RequestExecutor.ExecuteRequest<T>(url, method, body, Client, requestParameters);
        }
    }
}