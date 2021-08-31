using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class DeploymentEnvironmentTypeAssertions
    {
        public static DeploymentEnvironmentType ShouldBeFilled(this DeploymentEnvironmentType envType)
        {
            envType.ShouldNotBeNull();
            envType.type.ShouldBe("deployment_environment_type");
            envType.name.ShouldNotBeNullOrWhiteSpace();
            envType.rank.ShouldNotBeNull();

            return envType;
        }

        public static DeploymentEnvironmentType ShouldBeEquivalentTo(
            this DeploymentEnvironmentType envType,
            DeploymentEnvironmentType expectedEnvType)
        {
            if (expectedEnvType is null)
            {
                envType.ShouldBeNull();
            }
            else
            {
                envType.ShouldNotBeNull();
                envType.type.ShouldBe(expectedEnvType.type);
                envType.name.ShouldBe(expectedEnvType.name);
                envType.rank.ShouldBe(expectedEnvType.rank);
            }

            return envType;
        }
    }
}