using RestSharp;

namespace SharpBucket.Authentication
{
    public sealed class NoAuthentication: Authenticate
    {
        public NoAuthentication(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }
    }
}
