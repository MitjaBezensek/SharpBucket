using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelAssertions
    {
        public static BranchingModel ShouldBeFilled(this BranchingModel branchingModel)
        {
            branchingModel.ShouldNotBeNull();
            branchingModel.type.ShouldBe("branching_model");
            branchingModel.branch_types.ShouldBeFilled();
            branchingModel.development.ShouldBeFilled();
            branchingModel.production?.ShouldBeFilled();
            branchingModel.links.ShouldBeFilled();

            return branchingModel;
        }
    }
}
