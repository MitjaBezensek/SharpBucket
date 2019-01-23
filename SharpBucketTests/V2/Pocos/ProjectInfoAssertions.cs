using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class ProjectInfoAssertions
    {
        public static ProjectInfo ShouldBeFilled(this ProjectInfo projectInfo)
        {
            projectInfo.ShouldNotBeNull();
            projectInfo.key.ShouldNotBeNullOrEmpty();
            projectInfo.name.ShouldNotBeNullOrEmpty();
            projectInfo.uuid.ShouldNotBeNullOrEmpty();
            projectInfo.links.ShouldBeFilled();

            return projectInfo;
        }

        public static ProjectInfo ShouldBeEquivalentTo(this ProjectInfo projectInfo, ProjectInfo expectedProjectInfo)
        {
            if (expectedProjectInfo == null)
            {
                projectInfo.ShouldBeNull();
            }
            else
            {
                projectInfo.ShouldNotBeNull();
                projectInfo.key.ShouldBe(expectedProjectInfo.key);
                projectInfo.name.ShouldBe(expectedProjectInfo.name);
                projectInfo.uuid.ShouldBe(expectedProjectInfo.uuid);
                projectInfo.links.ShouldBeEquivalentTo(expectedProjectInfo.links);
            }


            return projectInfo;
        }
    }
}