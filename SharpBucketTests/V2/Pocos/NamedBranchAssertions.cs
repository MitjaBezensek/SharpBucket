using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class NamedBranchAssertions
    {
        public static NamedBranch ShouldBeFilled(this NamedBranch branch)
        {
            branch.ShouldNotBeNull();
            branch.name.ShouldNotBeNullOrEmpty();

            return branch;
        }
    }
}