using System.Collections.Generic;
using System.Linq;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SearchSegmentAssertions
    {
        public static List<SearchSegment> ShouldBeFilled(this List<SearchSegment> searchSegments)
        {
            searchSegments.ShouldNotBeNull();
            if (searchSegments.Count > 0)
            {
                searchSegments[0].ShouldBeFilled(); // check only the first one to speed up the tests. If first one is ok there is no reason than others are not.
                searchSegments.Count(s => s.match).ShouldBe(1, "A single segment should be a match in an collection of segments");
            }

            return searchSegments;
        }

        public static SearchSegment ShouldBeFilled(this SearchSegment searchSegment)
        {
            searchSegment.ShouldNotBeNull();
            searchSegment.text.ShouldNotBeNullOrEmpty();

            return searchSegment;
        }
    }
}