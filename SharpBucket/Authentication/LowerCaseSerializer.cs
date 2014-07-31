using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace SharpBucket.Authentication{
    public class LowerCaseSerializer : ISerializer, IDeserializer{
        public LowerCaseSerializer(){
            ContentType = "application/json";
        }

        public string Serialize(object obj){
            var settings = new JsonSerializerSettings{
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new LowerCaseResolver()
            };
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        public T Deserialize<T>(IRestResponse response){
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        string ISerializer.RootElement { get; set; }
        string ISerializer.Namespace { get; set; }
        string ISerializer.DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}