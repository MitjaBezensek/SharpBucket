using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcFileLinksAssertions
    {
        public static SrcFileLinks ShouldBeFilled(this SrcFileLinks srcLinks)
        {
            srcLinks.ShouldNotBeNull();
            srcLinks.self.ShouldBeFilled();
            srcLinks.meta.ShouldBeFilled();
            srcLinks.history.ShouldBeFilled();

            return srcLinks;
        }
    }
}
