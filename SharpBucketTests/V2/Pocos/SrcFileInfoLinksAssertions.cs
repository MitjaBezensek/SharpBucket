using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcFileInfoLinksAssertions
    {
        public static SrcFileInfoLinks ShouldBeFilled(this SrcFileInfoLinks srcLinks)
        {
            srcLinks.ShouldNotBeNull();
            srcLinks.self.ShouldBeFilled();

            return srcLinks;
        }
    }
}
