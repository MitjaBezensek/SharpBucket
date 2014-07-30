using RestSharp;

namespace SharpBucket.Authentication{
    internal class RequestExcecutor{
        public static string ExectueRequest<T>(string url, Method method, T body, RestClient client){
            var request = new RestRequest(url, method);
            if (ShouldAddBody(method)){
                request.AddObject(body);
            }
            return client.Execute(request).Content;
        }

        private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
        }
    }
}
