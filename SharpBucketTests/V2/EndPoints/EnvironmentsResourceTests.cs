using System.Linq;
using System.Net;
using SharpBucket.V2;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    using NUnit.Framework;

    [TestFixture]
    public class EnvironmentsResourceTests
    {
        [Test]
        public void PostEnvironment_RepositoryDoesNotExists_ThrowCorrectException()
        {
            var environmentsResource = SampleRepositories.NotExistingRepository.EnvironmentsResource;
            var environment = new DeploymentEnvironment
            {
                name = "testEnv",
                environment_type = DeploymentEnvironmentType.Test,
            };

            var exception = Should.Throw<BitbucketV2Exception>(
                () => environmentsResource.PostEnvironment(environment));

            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
            exception.Message.ShouldBe(exception.ErrorResponse.error.message);
        }

        [Test]
        public void PostEnvironment_NoEnvironmentType_ThrowCorrectException()
        {
            var environmentsResource = SampleRepositories.PrivateTestRepository.EnvironmentsResource;
            var environment = new DeploymentEnvironment
            {
                name = "testEnv",
            };

            var exception = Should.Throw<BitbucketV2Exception>(
                () => environmentsResource.PostEnvironment(environment));

            exception.HttpStatusCode.ShouldBe(HttpStatusCode.BadRequest);
            exception.Message.ShouldBe(exception.ErrorResponse.error.message);
            exception.ErrorResponse.error.id.ShouldNotBeNullOrWhiteSpace();
            exception.ErrorResponse.error.fields.Count.ShouldBe(1);
            exception.ErrorResponse.error.fields.First().Value.Length.ShouldBe(1);
        }

        [Test]
        public void PostAndListEnvironment_ElevenEnvironments_CreationWorksAndPaginationCorrectlyReturnAll()
        {
            var environmentsResource = SampleRepositories.PrivateTestRepository.EnvironmentsResource;

            // post eleven environments to go over the default pagination of 10 items
            for (var i = 0; i < 11; i++)
            {
                var environment = environmentsResource.PostEnvironment(
                    new DeploymentEnvironment
                    {
                        name = $"testEnv{i}",
                        environment_type = DeploymentEnvironmentType.Test,
                    });

                if (i == 0)
                {
                    environment.ShouldBeFilled();
                }
            }

            // check that listing correctly retrieve all items evn after default page
            var environments = environmentsResource.ListEnvironments();

            environments.ShouldNotBeNull();
            environments.Count.ShouldBe(11);
            environments[0].ShouldBeFilled();
        }
    }
}