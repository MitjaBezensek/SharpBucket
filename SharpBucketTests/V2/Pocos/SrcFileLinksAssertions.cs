using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class SrcFileLinksAssertions
    {
        public static SrcFileLinks ShouldBeFilled(this SrcFileLinks srcLinks)
        {
            srcLinks.ShouldNotBeNull();
            srcLinks.self.href.ShouldNotBeNull();
            srcLinks.meta.href.ShouldNotBeNull();
            srcLinks.history.href.ShouldNotBeNull();

            return srcLinks;
        }
    }
}
