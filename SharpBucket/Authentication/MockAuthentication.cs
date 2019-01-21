using RestSharp;
using System;

namespace SharpBucket.Authentication
{
    internal class MockAuthentication: Authenticate
    {
        public MockAuthentication(IRestClient client, string baseUrl)
        {
            client.BaseUrl = new Uri(baseUrl);
            this.client = client;
        }
    }
}
