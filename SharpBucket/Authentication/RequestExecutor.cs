using System.Collections.Generic;
using System.Net;
using RestSharp;
using RestSharp.Deserializers;

namespace SharpBucket.Authentication{
    internal class RequestExecutor{
        public static T ExecuteRequest<T>(string url, Method method, T body, RestClient client, IDictionary<string, object> requestParameters) where T : new(){
            var request = new RestRequest(url, method);
            if (requestParameters != null){
                foreach (var requestParameter in requestParameters){
                    request.AddParameter(requestParameter.Key, requestParameter.Value);
                }
            }

            if (ShouldAddBody(method)){
                request.RequestFormat = DataFormat.Json;
                request.AddObject(body);
            }

            //Fixed bug that prevents RestClient for adding custom headers to the request
            //https://stackoverflow.com/questions/22229393/why-is-restsharp-addheaderaccept-application-json-to-a-list-of-item

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
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