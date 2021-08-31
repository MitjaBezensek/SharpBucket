using System.Threading.Tasks;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    using NUnit.Framework;

    [TestFixture]
    public class EnvironmentResourceTests
    {
        [Test]
        public void GetEnvironment_ShouldMatchPostedOne_DeleteEnvironment_GetReturnARenamedEnvironment()
        {
            var environmentsResource = SampleRepositories.TestRepository.RepositoryResource.EnvironmentsResource;
            var environment = new DeploymentEnvironment
            {
                name = "testGetEnvironment",
                environment_type = DeploymentEnvironmentType.Test,
            };
            environment = environmentsResource.PostEnvironment(environment);

            var environmentResource = environmentsResource.EnvironmentResource(environment.uuid);

            var result = environmentResource.GetEnvironment();
            result.ShouldBeEquivalentTo(environment);

            environmentResource.DeleteEnvironment();

            var deletedEnv = environmentResource.GetEnvironment();
            deletedEnv.ShouldNotBeNull();
            deletedEnv.uuid.ShouldBe(environment.uuid);
            deletedEnv.name.ShouldNotBe(environment.name, "deleted env is in fact renamed, not deleted!");
            deletedEnv.slug.ShouldNotBe(environment.slug, "slug is changed accordingly");
            deletedEnv.hidden.ShouldBe(false, "deleted env is not even hidden!");
        }

        [Test]
        public async Task GetEnvironmentAsync_ShouldMatchPostedOne_DeleteEnvironmentAsync_GetReturnARenamedEnvironment()
        {
            var environmentsResource = SampleRepositories.TestRepository.RepositoryResource.EnvironmentsResource;
            var environment = new DeploymentEnvironment
            {
                name = "testGetEnvironmentAsync",
                environment_type = DeploymentEnvironmentType.Test,
            };
            environment = await environmentsResource.PostEnvironmentAsync(environment);

            var environmentResource = environmentsResource.EnvironmentResource(environment.uuid);

            var result = await environmentResource.GetEnvironmentAsync();
            result.ShouldBeEquivalentTo(environment);

            await environmentResource.DeleteEnvironmentAsync();

            var deletedEnv = await environmentResource.GetEnvironmentAsync();
            deletedEnv.ShouldNotBeNull();
            deletedEnv.uuid.ShouldBe(environment.uuid);
            deletedEnv.name.ShouldNotBe(environment.name, "deleted env is in fact renamed, not deleted!");
            deletedEnv.slug.ShouldNotBe(environment.slug, "slug is changed accordingly");
            deletedEnv.hidden.ShouldBe(false, "deleted env is not even hidden!");
        }
    }
}