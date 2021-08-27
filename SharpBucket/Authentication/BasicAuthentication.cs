using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class is used for basic authentication with the Bitbucket REST API.
    /// </summary>
    public sealed class BasicAuthentication : Authenticate
    {
        public BasicAuthentication(string username, string password, string baseUrl)
        {
            Client = new RestClient(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}