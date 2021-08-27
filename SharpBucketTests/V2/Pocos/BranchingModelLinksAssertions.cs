using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelLinksAssertions
    {
        public static BranchingModelLinks ShouldBeFilled(this BranchingModelLinks links)
        {
            links.ShouldNotBeNull();
            links.self.ShouldBeFilled();
            return links;
        }
    }
}