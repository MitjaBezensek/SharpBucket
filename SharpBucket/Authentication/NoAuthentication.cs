using RestSharp;

namespace SharpBucket.Authentication
{
    public class NoAuthentication: Authenticate
    {
        public NoAuthentication(string baseUrl)
        {
            client = new RestClient(baseUrl);
        }
    }
}
