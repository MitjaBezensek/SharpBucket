using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class WorkspaceAssertions
    {
        public static Workspace ShouldBeFilled(this Workspace workspace)
        {
            workspace.ShouldNotBeNull();
            workspace.type.ShouldBe("workspace");
            workspace.uuid.ShouldNotBe(default);
            workspace.name.ShouldNotBeNullOrEmpty();
            workspace.slug.ShouldNotBeNullOrEmpty();
            workspace.links.ShouldBeFilled();
            workspace.created_on.ShouldNotBe(default);

            return workspace;
        }

        public static Workspace ShouldBeEquivalentTo(this Workspace workspace, Workspace expectedWorkspace)
        {
            if (expectedWorkspace == null)
            {
                workspace.ShouldBeNull();
            }
            else
            {
                workspace.type.ShouldBe(expectedWorkspace.type);
                workspace.uuid.ShouldBe(expectedWorkspace.uuid);
                workspace.name.ShouldBe(expectedWorkspace.name);
                workspace.slug.ShouldBe(expectedWorkspace.slug);
                workspace.links.ShouldBeEquivalentTo(expectedWorkspace.links);
                workspace.created_on.ShouldBe(expectedWorkspace.created_on);
            }

            return workspace;
        }
    }
}
