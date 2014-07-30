using System;
using NServiceKit.ServiceHost;
using RestSharp;
using RestSharp.Serializers;

namespace SharpBucket.Authentication{
    internal class RequestExcecutor{
        public static string ExectueRequest<T>(string url, Method method, IReturn<T> body, RestClient client){
            var request = new RestRequest(url, method);
            if (ShouldAddBody(method)){
                request.JsonSerializer = new Serializer{ContentType = "application/json; charset=utf-8"};
                request.AddObject(body);
            }
            return client.Execute(request).Content;
        }

        private static bool ShouldAddBody(Method method){
            return method == Method.PUT || method == Method.POST;
        }

        private class Serializer : ISerializer{
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string DateFormat { get; set; }
            public string ContentType { get; set; }

            public string Serialize(object obj){
                using (new ConfigScope()){
                    return NServiceKit.Text.QueryStringSerializer.SerializeToString(obj);
                }
            }
        }
    }
}