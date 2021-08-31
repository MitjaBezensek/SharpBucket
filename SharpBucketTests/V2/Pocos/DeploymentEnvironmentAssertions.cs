using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class DeploymentEnvironmentAssertions
    {
        public static DeploymentEnvironment ShouldBeFilled(this DeploymentEnvironment environment)
        {
            environment.ShouldNotBeNull();
            environment.type.ShouldBe("deployment_environment");
            environment.uuid.ShouldNotBeNullOrWhiteSpace();
            environment.name.ShouldNotBeNullOrWhiteSpace();
            environment.slug.ShouldNotBeNullOrWhiteSpace();
            environment.deployment_gate_enabled.ShouldNotBeNull();
            environment.environment_lock_enabled.ShouldNotBeNull();
            environment.hidden.ShouldNotBeNull();
            environment.rank.ShouldNotBeNull();
            environment.category.ShouldBeFilled();
            environment.environment_type.ShouldBeFilled();
            environment.@lock.ShouldBeFilled();
            environment.restrictions.ShouldBeFilled();

            return environment;
        }

        public static DeploymentEnvironment ShouldBeEquivalentTo(
            this DeploymentEnvironment environment,
            DeploymentEnvironment expectedEnvironment)
        {
            if (expectedEnvironment is null)
            {
                environment.ShouldBeNull();
            }
            else
            {
                environment.ShouldNotBeNull();
                environment.type.ShouldBe(expectedEnvironment.type);
                environment.uuid.ShouldBe(expectedEnvironment.uuid);
                environment.name.ShouldBe(expectedEnvironment.name);
                environment.slug.ShouldBe(expectedEnvironment.slug);
                environment.deployment_gate_enabled.ShouldBe(expectedEnvironment.deployment_gate_enabled);
                environment.environment_lock_enabled.ShouldBe(expectedEnvironment.environment_lock_enabled);
                environment.hidden.ShouldBe(expectedEnvironment.hidden);
                environment.rank.ShouldBe(expectedEnvironment.rank);
                environment.category.ShouldBeEquivalentTo(expectedEnvironment.category);
                environment.environment_type.ShouldBeEquivalentTo(expectedEnvironment.environment_type);
                environment.@lock.ShouldBeEquivalentTo(expectedEnvironment.@lock);
                environment.restrictions.ShouldBeEquivalentTo(expectedEnvironment.restrictions);
            }

            return environment;
        }
    }
}
