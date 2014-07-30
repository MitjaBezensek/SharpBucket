using System;
using NServiceKit.ServiceHost;
using RestSharp;
using RestSharp.Serializers;

namespace SharpBucket.Authentication{
    internal class RequestExcecutor{
        public static string ExectueRequest<T>(string url, string method, IReturn<T> body, RestClient client){
            var request = new RestRequest(url, (Method) Enum.Parse(typeof (Method), method));
            if (method != "GET" && method != "DELETE"){
                request.JsonSerializer = new Serializer{ContentType = "application/json; charset=utf-8"};
                request.AddObject(body);
            }
            return client.Execute(request).Content;
        }

        private class Serializer : ISerializer{
            public string Serialize(object obj){
                using (new SharpBucket.ConfigScope()){
                    return NServiceKit.Text.QueryStringSerializer.SerializeToString(obj);
                }
            }

            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string DateFormat { get; set; }
            public string ContentType { get; set; }
        }
    }
}