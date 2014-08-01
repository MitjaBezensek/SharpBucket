using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace SharpBucket.Authentication{
    public class LowerCaseSerializer : ISerializer, IDeserializer{
        public readonly ITraceWriter traceWriter;
        private readonly JsonSerializerSettings settings;

        public LowerCaseSerializer(){
            ContentType = "application/json";
            traceWriter = new MemoryTraceWriter();
            settings = new JsonSerializerSettings{
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                ContractResolver = new LowerCaseResolver(),
                TraceWriter = traceWriter
            };
        }

        public string Serialize(object obj){
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        public T Deserialize<T>(IRestResponse response){
            return JsonConvert.DeserializeObject<T>(response.Content, settings);
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