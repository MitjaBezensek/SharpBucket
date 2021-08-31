using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class DeploymentEnvironmentCategoryAssertions
    {
        public static DeploymentEnvironmentCategory ShouldBeFilled(this DeploymentEnvironmentCategory category)
        {
            category.ShouldNotBeNull();
            category.name.ShouldNotBeNullOrWhiteSpace();

            return category;
        }

        public static DeploymentEnvironmentCategory ShouldBeEquivalentTo(
            this DeploymentEnvironmentCategory category,
            DeploymentEnvironmentCategory expectedCategory)
        {
            if (expectedCategory is null)
            {
                category.ShouldBeNull();
            }
            else
            {
                category.ShouldNotBeNull();
                category.name.ShouldBe(expectedCategory.name);
            }

            return category;
        }
    }
}