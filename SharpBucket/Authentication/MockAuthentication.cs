using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
