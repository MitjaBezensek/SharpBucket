using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class EnvironmentVariableAssertions
    {
        public static EnvironmentVariable ShouldBeFilled(this EnvironmentVariable variable)
        {
            variable.ShouldNotBeNull();
            variable.type?.ShouldBe("pipeline_variable"); // optional because type is not returned on POST (bitbucket issue?)
            variable.uuid.ShouldNotBeNullOrWhiteSpace();
            variable.key.ShouldNotBeNullOrWhiteSpace();
            if (variable.secured)
            {
                variable.value.ShouldBeNull();
            }
            else
            {
                variable.value.ShouldNotBeNullOrWhiteSpace();
            }

            return variable;
        }

        public static EnvironmentVariable ShouldBeEquivalentTo(
            this EnvironmentVariable variable,
            EnvironmentVariable expectedVariable)
        {
            if (expectedVariable is null)
            {
                variable.ShouldBeNull();
            }
            else
            {
                variable.ShouldNotBeNull();
                variable.type?.ShouldBe(expectedVariable.type ?? "pipeline_variable");
                variable.uuid.ShouldBe(expectedVariable.uuid);
                variable.key.ShouldBe(expectedVariable.key);
                variable.value.ShouldBe(expectedVariable.value);
                variable.secured.ShouldBe(expectedVariable.secured);
            }

            return variable;
        }
    }
}
