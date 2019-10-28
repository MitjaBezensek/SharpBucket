using RestSharp;
using System;

namespace SharpBucket.Authentication
{
    internal sealed class MockAuthentication: Authenticate
    {
        public MockAuthentication(IRestClient client, string baseUrl)
        {
            client.BaseUrl = new Uri(baseUrl);
            this.Client = client;
        }
    }
}
