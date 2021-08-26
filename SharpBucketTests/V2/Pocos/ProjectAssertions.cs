using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class ProjectAssertions
    {
        public static Project ShouldBeFilled(this Project project)
        {
            (project as ProjectInfo).ShouldBeFilled();
            project.has_publicly_visible_repos.ShouldNotBeNull();
            project.created_on.ShouldNotBeNull();
            project.updated_on.ShouldNotBeNull();
            project.description.ShouldNotBeNull();
            project.owner.ShouldBeFilled();
            project.workspace.ShouldBeFilled();

            return project;
        }

        public static Project ShouldBeEquivalentTo(this Project project, Project expectedProject)
        {
            if (expectedProject == null)
            {
                project.ShouldBeNull();
            }
            else
            {
                (project as ProjectInfo).ShouldBeEquivalentTo(expectedProject);
                project.created_on.ShouldBe(expectedProject.created_on);
                project.updated_on.ShouldBe(expectedProject.updated_on);
                project.description.ShouldBe(expectedProject.description);
                project.owner.ShouldBeEquivalentTo(expectedProject.owner);
                project.workspace.ShouldBeEquivalentTo(expectedProject.workspace);
            }

            return project;
        }

        public static Project ShouldBeEquivalentExceptUpdateDateTo(this Project project, Project expectedProject)
        {
            if (expectedProject == null)
            {
                project.ShouldBeNull();
            }
            else
            {
                (project as ProjectInfo).ShouldBeEquivalentExceptAvatarTimeStampTo(expectedProject);
                project.created_on.ShouldBe(expectedProject.created_on);
                project.updated_on.GetValueOrDefault().ShouldBeGreaterThan(expectedProject.updated_on.GetValueOrDefault());
                project.description.ShouldBe(expectedProject.description);
                project.owner.ShouldBeEquivalentTo(expectedProject.owner);
                project.workspace.ShouldBeEquivalentTo(expectedProject.workspace);
            }

            return project;
        }
    }
}
