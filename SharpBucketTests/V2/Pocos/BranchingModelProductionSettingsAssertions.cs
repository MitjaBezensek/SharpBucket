using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelProductionSettingsAssertions
    {
        public static BranchingModelProductionSettings ShouldBeFilled(this BranchingModelProductionSettings productionSettings)
        {
            productionSettings.ShouldNotBeNull();
            if (productionSettings.use_mainbranch)
            {
                productionSettings.name.ShouldBeNull();
            }
            else
            {
                productionSettings.name.ShouldNotBeNullOrWhiteSpace();
            }
            return productionSettings;
        }
    }
}