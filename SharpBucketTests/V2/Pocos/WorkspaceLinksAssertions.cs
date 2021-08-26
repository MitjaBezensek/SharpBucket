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

        public static WorkspaceLinks ShouldBeEquivalentTo(this WorkspaceLinks links, WorkspaceLinks expectedLinks)
        {
            if (expectedLinks == null)
            {
                links.ShouldBeNull();
            }
            else
            {
                links.avatar.ShouldBeEquivalentTo(expectedLinks.avatar);
                links.hooks.ShouldBeEquivalentTo(expectedLinks.hooks);
                links.html.ShouldBeEquivalentTo(expectedLinks.html);
                links.members.ShouldBeEquivalentTo(expectedLinks.members);
                links.owners.ShouldBeEquivalentTo(expectedLinks.owners);
                links.projects.ShouldBeEquivalentTo(expectedLinks.projects);
                links.repositories.ShouldBeEquivalentTo(expectedLinks.repositories);
                links.self.ShouldBeEquivalentTo(expectedLinks.self);
                links.snippets.ShouldBeEquivalentTo(expectedLinks.snippets);
            }

            return links;
        }
    }
}
