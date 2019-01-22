using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class is used for basic authentication with the BitBucket REST API.
    /// </summary>
    public class BasicAuthentication : Authenticate
    {
        protected override IRestClient Client { get; }

        public BasicAuthentication(string username, string password, string baseUrl)
        {
            Client = new RestClient(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}