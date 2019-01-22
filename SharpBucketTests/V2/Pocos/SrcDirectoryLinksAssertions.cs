using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcDirectoryLinksAssertions
    {
        public static SrcDirectoryLinks ShouldBeFilled(this SrcDirectoryLinks srcDirectoryLinks)
        {
            srcDirectoryLinks.ShouldNotBeNull();
            srcDirectoryLinks.self.href.ShouldNotBeNull();
            srcDirectoryLinks.meta.href.ShouldNotBeNull();

            return srcDirectoryLinks;
        }
    }
}
