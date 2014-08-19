using System.Diagnostics;
using RestSharp;

namespace SharpBucket.Authentication{
    internal class RequestExecutor{
        private static readonly LowerCaseSerializer serializer = new LowerCaseSerializer();

        public static T ExecuteRequest<T>(string url, Method method, T body, RestClient client) where T : new(){
            client.AddHandler("application/json", serializer);
            var request = new RestRequest(url, method);
            if (ShouldAddBody(method)){
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = serializer;
                request.AddObject(body);
            }
            var result = client.Execute<T>(request);
            Debug.WriteLine(serializer.traceWriter);
            return result.Data;
        }

        private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
        }
    }
}