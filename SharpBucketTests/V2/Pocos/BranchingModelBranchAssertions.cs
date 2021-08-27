using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelBranchAssertions
    {
        public static BranchingModelBranch ShouldBeFilled(this BranchingModelBranch branchingModelBranch)
        {
            branchingModelBranch.ShouldNotBeNull();
            if (branchingModelBranch.use_mainbranch)
            {
                branchingModelBranch.name.ShouldBeNull();
                branchingModelBranch.branch.ShouldBeNull();
            }
            else
            {
                branchingModelBranch.name.ShouldNotBeNullOrWhiteSpace();
                branchingModelBranch.branch.ShouldBeFilled();
            }
            return branchingModelBranch;
        }
    }
}