using System;

namespace SharpBucket.Authentication
{    
    public sealed class ResponseExaminer {
        public int StatusCode { get; private set; }
        public string Body { get; private set; }

        public static implicit operator Action<IRestResponseExaminer>(ResponseExaminer instance){
             return r =>{
                 r.CollectStatus(s => instance.StatusCode = s);
                 r.CollectBody(b => instance.Body = b);
             };
         }
    }
}
