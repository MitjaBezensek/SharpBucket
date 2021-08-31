using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class DeploymentEnvironmentLockAssertions
    {
        public static DeploymentEnvironmentLock ShouldBeFilled(this DeploymentEnvironmentLock envLock)
        {
            envLock.ShouldNotBeNull();
            envLock.type.ShouldStartWith("deployment_environment_lock");
            envLock.name.ShouldNotBeNullOrWhiteSpace();

            return envLock;
        }

        public static DeploymentEnvironmentLock ShouldBeEquivalentTo(
            this DeploymentEnvironmentLock envLock,
            DeploymentEnvironmentLock expectedEnvLock)
        {
            if (expectedEnvLock is null)
            {
                envLock.ShouldBeNull();
            }
            else
            {
                envLock.ShouldNotBeNull();
                envLock.type.ShouldBe(expectedEnvLock.type);
                envLock.name.ShouldBe(expectedEnvLock.name);
            }

            return envLock;
        }
    }
}