using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using SharpBucket.Authentication;

namespace SharpBucket.V2
{
    internal class RequestExecutorV2 : RequestExecutor
    {
        protected override void AddBody(RestRequest request, object body)
        {
            // Use Newtonsoft.Json serialization instead of the one included in RestSharp to be able to
            // ignore null properties during the serialization
            // https://stackoverflow.com/questions/20006813/restsharp-how-to-skip-serializing-null-values-to-json
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var jsonString = JsonConvert.SerializeObject(body, jsonSettings);
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        }
    }
}
