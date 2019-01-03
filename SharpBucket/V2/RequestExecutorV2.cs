using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using SharpBucket.Authentication;
using SimpleJson;

namespace SharpBucket.V2
{
    internal class RequestExecutorV2 : RequestExecutor
    {
        protected override void AddBody(RestRequest request, object body)
        {
            // Use a custom JsonSerializerStrategy to be able to ignore null properties during the serialization
            // https://stackoverflow.com/questions/20006813/restsharp-how-to-skip-serializing-null-values-to-json
            var jsonString = SimpleJson.SimpleJson.SerializeObject(body, new JsonSerializerStrategy());
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);
        }

        private class JsonSerializerStrategy : PocoJsonSerializerStrategy
        {
            protected override object SerializeEnum(Enum p)
            {
                return p.ToString();
            }

            protected override bool TrySerializeUnknownTypes(object input, out object output)
            {
                var returnValue = base.TrySerializeUnknownTypes(input, out output);

                if (output is IDictionary<string, object> obj)
                {
                    output = obj.Where(o => o.Value != null).ToDictionary(o => o.Key, o => o.Value);
                }

                return returnValue;
            }
        }
    }
}
