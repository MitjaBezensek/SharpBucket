using RestSharp;

namespace SharpBucket.Authentication{
    internal class RequestExecutor{
        public static T ExecuteRequest<T>(string url, Method method, T body, RestClient client) where T : new(){
            var request = new RestRequest(url, method);
            if (ShouldAddBody(method)){
                request.RequestFormat = DataFormat.Json;
                request.AddObject(body);
            }
            var result = client.Execute<T>(request);
            return result.Data;
        }

        private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
        }
    }
}