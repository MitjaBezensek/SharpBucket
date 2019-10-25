using System.Collections.Generic;
using System.Linq;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SearchLineAssertions
    {
        public static List<SearchLine> ShouldBeFilled(this List<SearchLine> searchLines)
        {
            searchLines.ShouldNotBeNull();
            searchLines.Count.ShouldBePositive();

            // Check only the first one to speed up the tests.
            // And also one line that have segments if the first one do not have some.
            // There is no reason tha other lines would not be filled.
            searchLines[0].ShouldBeFilled();
            if (searchLines[0].segments.Count == 0)
            {
                searchLines.FirstOrDefault(l => l.segments.Count > 0)?.ShouldBeFilled();
            }

            return searchLines;
        }

        public static SearchLine ShouldBeFilled(this SearchLine searchLine)
        {
            searchLine.ShouldNotBeNull();
            searchLine.line.ShouldBePositive();
            searchLine.segments.ShouldBeFilled();

            return searchLine;
        }
    }
}