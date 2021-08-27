using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class BranchingModelDevelopmentSettingsAssertions
    {
        public static BranchingModelDevelopmentSettings ShouldBeFilled(this BranchingModelDevelopmentSettings developmentSettings)
        {
            developmentSettings.ShouldNotBeNull();
            if (developmentSettings.use_mainbranch)
            {
                developmentSettings.name.ShouldBeNull();
            }
            else
            {
                developmentSettings.name.ShouldNotBeNullOrWhiteSpace();
            }
            return developmentSettings;
        }
    }
}