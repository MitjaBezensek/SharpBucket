using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace SharpBucket.Authentication
{
    public sealed class RestClientRestResponseExaminer : IRestResponseExaminer{

        private readonly IRestResponse response;
        
        private List<Action<RestClientRestResponseExaminer>> responseActions = new List<Action<RestClientRestResponseExaminer>>();

        private RestClientRestResponseExaminer(IRestResponse response){
            this.response = response;
        }

        public RestClientRestResponseExaminer(){ }

        public void ExamineResponse(IRestResponse requestToConfigure){
            var instance = new RestClientRestResponseExaminer(requestToConfigure) { responseActions = responseActions };
            instance.ExamineResponse();
        }

        private void ExamineResponse(){            
            foreach (var configurator in responseActions){
                configurator(this);
            }
        }

        public IRestResponseExaminer CollectStatus(Action<int> status){
            responseActions.Add(r =>{
                status((int)r.response.StatusCode);
            });
            return this;
        }

        public IRestResponseExaminer CollectBody(Action<string> body){
            responseActions.Add(r =>{
                body(r.response.Content);
            });
            return this;
        }

        public IRestResponseExaminer CollectHeaders(Action<string, object> collect){
            responseActions.Add(r =>{                
                foreach (var h in r.response.Headers.Where(x => x.Type == ParameterType.HttpHeader)){
                    collect(h.Name, h.Value);
                }
            });
            return this;
        }
    }
}