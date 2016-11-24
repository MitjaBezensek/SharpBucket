using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace SharpBucket.Authentication{
    internal class RequestExecutor{
        public static T ExecuteRequest<T>(string url, Method method, T body, RestClient client, Dictionary<string, object> requestParameters) where T : new(){
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

        public static T ExecuteRequestAndExamineBody<T>(string url, Method method, T body, RestClient client, Dictionary<string, object> requestParameters, Action<IRestResponseExaminer> configureResponseExaminer) where T : new(){
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
            var result = client.Execute<T>(request);
            
            var responseExaminerConfigurator = configureResponseExaminer ?? (r => { });

            var responseExaminer = new RestClientRestResponseExaminer();
            responseExaminerConfigurator(responseExaminer);
            responseExaminer.ExamineResponse(result);
            
            if (result.ErrorException != null){
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