using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Moq;
using RestSharp;
using SharpBucket.V2;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    public class MockAuthenticationTests
    {
        private Mock<IRestClient> _client;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IRestClient>();
        }

        [Test]
        public void Create_ShouldUseMock()
        {
            string expected = "Hello from mock";

            _client.Setup(c => c.Execute<object>(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse<object>()
                {
                    ResponseStatus = ResponseStatus.Completed,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = expected,
                });

            var client = new SharpBucketV2();
            client.MockAuthentication(_client.Object);
            var output = client.Get(new object(), null);
            output.ShouldBe(expected);
        }
    }
}
