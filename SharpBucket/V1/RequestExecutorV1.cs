using RestSharp;
using SharpBucket.Authentication;

namespace SharpBucket.V1
{
    internal class RequestExecutorV1 : RequestExecutor
    {
        protected override void AddBody(IRestRequest request, object body)
        {
            request.AddObject(body);
        }
    }
}
