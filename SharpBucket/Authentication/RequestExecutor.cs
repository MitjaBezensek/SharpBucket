using System.Net;
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

            if (result.ErrorException != null) {
                throw new WebException("REST client encountered an error: " + result.ErrorMessage, result.ErrorException);
            }
            // This is a hack in order to allow this method to work for simple types as well
            // one example of this is the GetRevisionRaw method
            if (RequestingSimpleType<T>()){
              return result.Content as dynamic;
            }
            return result.Data;
        }

       private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
       }

       private static bool RequestingSimpleType<T>() where T : new(){
          return typeof(T) == typeof(object);
       }
    }
}