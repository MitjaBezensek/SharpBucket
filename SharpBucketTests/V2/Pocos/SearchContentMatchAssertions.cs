using System.Collections.Generic;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SearchContentMatchAssertions
    {
        public static List<SearchContentMatch> ShouldBeFilled(this List<SearchContentMatch> searchContentMatches)
        {
            searchContentMatches.ShouldNotBeNull();
            searchContentMatches.Count.ShouldBePositive();
            searchContentMatches[0].ShouldBeFilled(); // check only the first one to speed up the tests. If first one is ok there is no reason than others are not.

            return searchContentMatches;
        }

        public static SearchContentMatch ShouldBeFilled(this SearchContentMatch searchContentMatch)
        {
            searchContentMatch.ShouldNotBeNull();
            searchContentMatch.lines.ShouldBeFilled();

            return searchContentMatch;
        }
    }
}
