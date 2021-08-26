using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;

namespace SharpBucketTests.V2.EndPoints
{
    public class SearchCodeResourceTests
    {
        [Test]
        public void ListSearchResults_SearchStringWordFromTeamAtlassian_ShouldReturnAtLeastOneResult()
        {
            var atlassianWorkspace = TestHelpers.SharpBucketV2.WorkspacesEndPoint().WorkspaceResource("atlassian");
            var searchResults = atlassianWorkspace.SearchCodeResource.ListSearchResults("string", max: 2);
            var firstResult = searchResults.FirstOrDefault();
            firstResult.ShouldBeFilled();
        }

        [Test]
        public void EnumerateSearchResults_SearchStringWordFromTeamAtlassian_ShouldReturnAtLeastOneResult()
        {
            var atlassianWorkspace = TestHelpers.SharpBucketV2.WorkspacesEndPoint().WorkspaceResource("atlassian");
            var searchResults = atlassianWorkspace.SearchCodeResource.EnumerateSearchResults("string", pageLen: 2);
            var firstResult = searchResults.FirstOrDefault();
            firstResult.ShouldBeFilled();
        }

        [Test]
        public void EnumerateSearchResults_SearchStringWordFromTeamAtlassianWithPageLenLessThanTheNumberOfEnumeratedResults_RequestsCountShouldIncrementLazily()
        {
            ISharpBucketRequesterV2 realSharpBucketRequesterV2 = TestHelpers.SharpBucketV2;
            var sharpBucketRequesterV2Mock = new Mock<ISharpBucketRequesterV2>();
            Expression<Func<ISharpBucketRequesterV2, IteratorBasedPage<SearchCodeSearchResult>>> sendMethod
                = x => x.Send<IteratorBasedPage<SearchCodeSearchResult>>(It.IsAny<HttpMethod>(), It.IsAny<object>(), It.IsAny<string>(), It.IsAny<object>());
            sharpBucketRequesterV2Mock
                .Setup(sendMethod)
                .Returns<HttpMethod, object, string, object>((m, b, u, p) => realSharpBucketRequesterV2.Send<IteratorBasedPage<SearchCodeSearchResult>>(m, b, u, p));
            var workspacesEndPointIntercepted = new WorkspacesEndPoint(sharpBucketRequesterV2Mock.Object);

            var searchResults = workspacesEndPointIntercepted.WorkspaceResource("atlassian").SearchCodeResource.EnumerateSearchResults("string", pageLen: 5);

            sharpBucketRequesterV2Mock.Verify(
                sendMethod,
                Times.Never(),
                "Building the enumerable should not produce any request");

            var i = 0;
            foreach (var _ in searchResults)
            {
                if (i < 5)
                {
                    sharpBucketRequesterV2Mock.Verify(
                        sendMethod,
                        Times.Exactly(1),
                        "Only first page should have been called");
                }
                else
                {
                    sharpBucketRequesterV2Mock.Verify(
                        sendMethod,
                        Times.Exactly(2),
                        "Only two pages should have been called");
                    if (i == 9)
                    {
                        break;
                    }
                }

                i++;
            }
        }

        [Test]
        public async Task EnumerateSearchResultsAsync_SearchStringWordFromTeamAtlassian_ShouldReturnAtLeastOneResult()
        {
            var atlassianWorkspace = TestHelpers.SharpBucketV2.WorkspacesEndPoint().WorkspaceResource("atlassian");
            var searchResults = atlassianWorkspace.SearchCodeResource.EnumerateSearchResultsAsync("string");
            var firstResult = await searchResults.FirstOrDefaultAsync();
            firstResult.ShouldBeFilled();
        }
    }
}
