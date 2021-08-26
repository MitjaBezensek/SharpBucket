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
    }
}
