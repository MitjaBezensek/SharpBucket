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


        // Method created only as a workaround for the Bitbucket issue reported here:
        // https://getsupport.atlassian.com/servicedesk/customer/portal/11/BBS-131627?error=login_required&error_description=Login+required&state=29f02f9e-f858-44c9-bc3a-9468e8db6322
        public static ProjectInfo ShouldBeEquivalentExceptAvatarTimeStampTo(this ProjectInfo projectInfo, ProjectInfo expectedProjectInfo)
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
                projectInfo.links.ShouldBeEquivalentExceptAvatarTimeStampTo(expectedProjectInfo.links);
            }


            return projectInfo;
        }
    }
}