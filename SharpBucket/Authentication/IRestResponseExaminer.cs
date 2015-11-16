using System;

namespace SharpBucket.Authentication
{    
    public interface IRestResponseExaminer{
        IRestResponseExaminer CollectHeaders(Action<string, object> collect);
        IRestResponseExaminer CollectStatus(Action<int> status);
        IRestResponseExaminer CollectBody(Action<string> status);
    }
}