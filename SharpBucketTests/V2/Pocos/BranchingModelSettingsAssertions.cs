using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelSettingsAssertions
    {
        public static BranchingModelSettings ShouldBeFilled(this BranchingModelSettings branchingModelSettings)
        {
            branchingModelSettings.ShouldNotBeNull();
            branchingModelSettings.type.ShouldBe("branching_model_settings");
            branchingModelSettings.branch_types.ShouldBeFilled();
            branchingModelSettings.development.ShouldBeFilled();
            branchingModelSettings.production?.ShouldBeFilled();
            branchingModelSettings.links.ShouldBeFilled();

            return branchingModelSettings;
        }
    }
}