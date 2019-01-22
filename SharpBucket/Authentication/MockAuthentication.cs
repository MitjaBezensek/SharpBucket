using RestSharp;
using System;

namespace SharpBucket.Authentication
{
    internal class MockAuthentication: Authenticate
    {
        protected override IRestClient Client { get; }

        public MockAuthentication(IRestClient client, string baseUrl)
        {
            client.BaseUrl = new Uri(baseUrl);
            this.Client = client;
        }
    }
}
