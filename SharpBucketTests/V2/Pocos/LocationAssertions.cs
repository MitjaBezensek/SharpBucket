using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class LocationAssertions
    {
        public static Location ShouldBeFilled(this Location location)
        {
            location.ShouldNotBeNull();
            location.path.ShouldNotBeNullOrEmpty();
            ////location.from is not mandatory if not on a specific line in the file
            ////location.to is not mandatory if not on a specific line in the file

            return location;
        }
    }
}