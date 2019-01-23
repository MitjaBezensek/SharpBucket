using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommitLinksAssertions
    {
        public static CommitLinks ShouldBeFilled(this CommitLinks commitLinks)
        {
            commitLinks.ShouldNotBeNull();
            commitLinks.self.href.ShouldNotBeNull();
            commitLinks.html.href.ShouldNotBeNull();

            return commitLinks;
        }
    }
}
