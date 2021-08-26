using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class WorkspaceInfoAssertions
    {
        public static WorkspaceInfo ShouldBeFilled(this WorkspaceInfo workspace)
        {
            workspace.ShouldNotBeNull();
            workspace.type.ShouldBe("workspace");
            workspace.uuid.ShouldNotBe(default);
            workspace.name.ShouldNotBeNullOrEmpty();
            workspace.slug.ShouldNotBeNullOrEmpty();
            workspace.links.ShouldBeFilled();

            return workspace;
        }

        public static WorkspaceInfo ShouldBeEquivalentTo(this WorkspaceInfo workspace, WorkspaceInfo expectedWorkspace)
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
            }

            return workspace;
        }
    }
}