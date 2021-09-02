using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class EnvironmentVariablesResourceTests
    {
        /// <summary>
        /// Bitbucket seems to need some times before the list to be updated.
        /// It's why we wait using this constant at some points.
        /// </summary>
        private const int CREATE_VAR_WAIT_DURATION = 1000;

        private DeploymentEnvironment Environment { get; set; }
        private DeploymentsConfigEnvironmentResource EnvironmentResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var repositoryResource = SampleRepositories.TestRepository.RepositoryResource;
            var environmentsResource = repositoryResource.EnvironmentsResource;
            var environment = new DeploymentEnvironment
            {
                name = "testEnvironmentVariablesResourceTests",
                environment_type = DeploymentEnvironmentType.Test,
            };
            this.Environment = environmentsResource.PostEnvironment(environment);
            this.EnvironmentResource = repositoryResource
                                       .DeploymentsConfigResource
                                       .EnvironmentsResource
                                       .EnvironmentResource(this.Environment.uuid);
        }

        [Test]
        public void PostAndListEnvironmentVariables_ValidParameters_Works()
        {
            var variablesResource = this.EnvironmentResource.VariablesResource;

            // post one variable
            var variable = variablesResource.PostVariable(
                new EnvironmentVariable
                {
                    key = "foo",
                    value = "bar",
                });
            var variableResource = variablesResource.VariableResource(variable.uuid);

            try
            {
                variable.ShouldBeFilled();
                variable.key.ShouldBe("foo");
                variable.value.ShouldBe("bar");
                variable.secured.ShouldBe(false);

                // check that listing correctly retrieve that variable
                Thread.Sleep(CREATE_VAR_WAIT_DURATION);
                var variables = variablesResource.ListVariables();
                variables.ShouldNotBeNull();
                variables.Count.ShouldBe(1);
                variables[0].ShouldBeEquivalentTo(variable);

                // test update
                variable.value = "barBar";
                var updatedVariable = variableResource.PutVariable(variable);
                updatedVariable.value.ShouldBe("barBar");

                // check it's updated
                variables = variablesResource.ListVariables();
                variables.ShouldNotBeNull();
                variables.Count.ShouldBe(1);
                variables[0].ShouldBeEquivalentTo(updatedVariable);
            }
            finally
            {
                // delete the variable in a finally block to avoid to let bad data behind us.
                variableResource.DeleteVariable();
                Thread.Sleep(CREATE_VAR_WAIT_DURATION);
            }
        }

        [Test]
        public void EnumerateEnvironmentVariables_3CreatedAndPageLenIs2_AllPagesAreEnumerated()
        {
            var variablesResource = this.EnvironmentResource.VariablesResource;

            var createdVariables = new List<EnvironmentVariable>();
            try
            {
                // post variables
                for (var i = 0; i < 3; i++)
                {
                    var newVar = variablesResource.PostVariable(
                        new EnvironmentVariable
                        {
                            key = $"foo{i}",
                            value = "bar",
                        });
                    createdVariables.Add(newVar);
                }

                // check that listing correctly retrieve that variable
                Thread.Sleep(createdVariables.Count * CREATE_VAR_WAIT_DURATION);
                var variables = variablesResource.EnumerateVariables(createdVariables.Count - 1).ToList();
                variables.ShouldNotBeNull();
                variables.Count.ShouldBe(createdVariables.Count);
            }
            finally
            {
                // delete the variables in a finally block to avoid to let bad data behind us.
                foreach (var variable in createdVariables)
                {
                    variablesResource.VariableResource(variable.uuid).DeleteVariable();
                }
                Thread.Sleep(createdVariables.Count * CREATE_VAR_WAIT_DURATION);
            }
        }
    }
}
