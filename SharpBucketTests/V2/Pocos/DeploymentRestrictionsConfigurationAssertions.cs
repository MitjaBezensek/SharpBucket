using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class DeploymentRestrictionsConfigurationAssertions
    {
        public static DeploymentRestrictionsConfiguration ShouldBeFilled(
            this DeploymentRestrictionsConfiguration restrictionsConf)
        {
            restrictionsConf.ShouldNotBeNull();
            restrictionsConf.type.ShouldStartWith("deployment_restrictions_configuration");
            restrictionsConf.admin_only.ShouldNotBeNull();

            return restrictionsConf;
        }

        public static DeploymentRestrictionsConfiguration ShouldBeEquivalentTo(
            this DeploymentRestrictionsConfiguration restrictionsConf,
            DeploymentRestrictionsConfiguration expectedRestrictionsConf)
        {
            if (expectedRestrictionsConf is null)
            {
                restrictionsConf.ShouldBeNull();
            }
            else
            {
                restrictionsConf.ShouldNotBeNull();
                restrictionsConf.type.ShouldBe(expectedRestrictionsConf.type);
                restrictionsConf.admin_only.ShouldBe(expectedRestrictionsConf.admin_only);
            }

            return restrictionsConf;
        }
    }
}