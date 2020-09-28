using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchAssertions
    {
        public static Branch ShouldBeFilled(this Branch branch)
        {
            branch.ShouldNotBeNull();
            branch.name.ShouldNotBeNullOrEmpty();
            branch.target.ShouldBeFilled();
            branch.links.ShouldNotBeNull();
            branch.links.commits.ShouldBeFilled();
            branch.links.html.ShouldBeFilled();
            branch.links.self.ShouldBeFilled();

            return branch;
        }
    }
}
