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

            _client.Setup(c => c.Execute(It.IsAny<IRestRequest>()))
                .Returns(new RestResponse()
                {
                    ResponseStatus = ResponseStatus.Completed,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = expected,
                });

            var client = new SharpBucketV2();
            client.MockAuthentication(_client.Object);
            var output = client.Get(null);
            output.ShouldBe(expected);
        }
    }
}
