using RestSharp;
using RestSharp.Authenticators;

namespace SharpBucket.Authentication
{
    /// <summary>
    /// This class is used for basic authetication with the BitBucket REST API.
    /// </summary>
    public class BasicAuthentication : Authenticate
    {
        public BasicAuthentication(string username, string password, string baseUrl)
        {
            client = new RestClient(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };
        }
    }
}