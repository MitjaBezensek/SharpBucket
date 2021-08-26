using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class WorkspaceLinksAssertions
    {
        public static WorkspaceLinks ShouldBeFilled(this WorkspaceLinks links)
        {
            links.ShouldNotBeNull();
            links.avatar.ShouldBeFilled();
            links.hooks.ShouldBeFilled();
            links.html.ShouldBeFilled();
            links.members.ShouldBeFilled();
            links.owners.ShouldBeFilled();
            links.projects.ShouldBeFilled();
            links.repositories.ShouldBeFilled();
            links.self.ShouldBeFilled();
            links.snippets.ShouldBeFilled();

            return links;
        }
    }
}
