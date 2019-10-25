using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SearchCodeSearchResultAssertions
    {
        public static SearchCodeSearchResult ShouldBeFilled(this SearchCodeSearchResult searchCodeSearchResult)
        {
            searchCodeSearchResult.ShouldNotBeNull();
            searchCodeSearchResult.type.ShouldBe("code_search_result");
            searchCodeSearchResult.content_match_count.ShouldBePositive();
            searchCodeSearchResult.content_matches.ShouldBeFilled();
            searchCodeSearchResult.path_matches.ShouldBeFilled();
            searchCodeSearchResult.file.ShouldBeFilled();

            return searchCodeSearchResult;
        }
    }
}