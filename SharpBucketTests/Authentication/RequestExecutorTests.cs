using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using SharpBucket;
using SharpBucket.Authentication;
using SharpBucket.V2;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.Authentication
{
    [TestFixture]
    public class RequestExecutorTests
    {
        [Test]
        public void ExecuteRequest_BadRequest_BitbucketExceptionShouldBeThrow()
        {
            var client = new RestClient(SharpBucketV2.BITBUCKET_URL);
            var requestExecutor = new FakeRequestExecutor();

            var exception = Should.Throw<BitbucketException>(
                () => requestExecutor.ExecuteRequest<IteratorBasedPage<SearchCodeSearchResult>>(
                    "teams/Atlassian/search/code",
                    Method.GET,
                    null,
                    client,
                    new Dictionary<string, object>
                    {
                        {"bad_search_query", "string"},
                    }),
                "Using a bad query should produce a BitBucketException, since it's Bitbucket that detect the error and say it to us.'");
            exception.Message.ShouldBe("{\"type\": \"error\", \"error\": {\"message\": \"You must provide a search query\"}}");
        }

        [Test]
        public void ExecuteRequest_PocoTypeCouldNotBeMapped_RealExceptionShouldBeThrow()
        {
            var client = new RestClient(SharpBucketV2.BITBUCKET_URL);
            var requestExecutor = new FakeRequestExecutor();

            var exception = Should.Throw<Exception>(
                () => requestExecutor.ExecuteRequest<IteratorBasedPage<BadSearchCodeSearchResult>>(
                            "teams/Atlassian/search/code",
                            Method.GET,
                            null,
                            client,
                            new Dictionary<string, object>
                            {
                                {"search_query", "string"},
                            }),
                "Using a bad POCO should produce an Exception, but not a BitbucketException since it's not an issue detected by BitBucket, but an issue in our code.");
            exception.Message.ShouldBe("No parameterless constructor defined for type 'SharpBucket.V2.Pocos.SearchContentMatch[]'.");
        }

        private class FakeRequestExecutor : RequestExecutor
        {
            protected override void AddBody(IRestRequest request, object body)
            {
                throw new NotImplementedException();
            }
        }

        private class BadSearchCodeSearchResult
        {
            public SearchContentMatch[] content_matches { get; set; }
        }
    }
}
