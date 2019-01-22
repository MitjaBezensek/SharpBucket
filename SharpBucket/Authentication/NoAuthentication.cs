using RestSharp;

namespace SharpBucket.Authentication
{
    public class NoAuthentication: Authenticate
    {
        protected override IRestClient Client { get; }

        public NoAuthentication(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }
    }
}
