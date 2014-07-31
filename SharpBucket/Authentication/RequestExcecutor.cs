using Newtonsoft.Json;
using RestSharp;

namespace SharpBucket.Authentication{
    internal class RequestExcecutor{
        public static T ExectueRequest<T>(string url, Method method, T body, RestClient client) where T : new(){
            var request = new RestRequest(url, method);
            if (ShouldAddBody(method)){
                request.AddObject(body);
            }
            var settings = new JsonSerializerSettings{
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new LowerCaseResolver()
            };
            var test = client.Execute(request);
            var result =  JsonConvert.DeserializeObject<T>(test.Content, settings);
            return result;
        }

        private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
        }
    }
}