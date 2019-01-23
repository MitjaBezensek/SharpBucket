using System;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class ProjectAssertions
    {
        public static Project ShouldBeFilled(this Project project)
        {
            (project as ProjectInfo).ShouldBeFilled();
            project.created_on.ShouldNotBeNullOrEmpty();
            project.updated_on.ShouldNotBeNullOrEmpty();
            project.description.ShouldNotBeNull();
            project.owner.ShouldBeFilled();

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
                (project as ProjectInfo).ShouldBeEquivalentTo(expectedProject);
                project.created_on.ShouldBe(expectedProject.created_on);
                DateTime.Parse(project.updated_on).ShouldBeGreaterThan(DateTime.Parse(expectedProject.updated_on));
                project.description.ShouldBe(expectedProject.description);
                project.owner.ShouldBeEquivalentTo(expectedProject.owner);
                
            }

            return project;
        }
    }
}
