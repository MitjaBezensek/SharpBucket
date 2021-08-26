using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class WorkspaceInfoLinksAssertions
    {
        public static WorkspaceInfoLinks ShouldBeFilled(this WorkspaceInfoLinks links)
        {
            links.ShouldNotBeNull();
            links.avatar.ShouldBeFilled();
            links.html.ShouldBeFilled();
            links.self.ShouldBeFilled();

            return links;
        }

        public static WorkspaceInfoLinks ShouldBeEquivalentTo(this WorkspaceInfoLinks links, WorkspaceInfoLinks expectedLinks)
        {
            if (expectedLinks == null)
            {
                links.ShouldBeNull();
            }
            else
            {
                links.avatar.ShouldBeEquivalentTo(expectedLinks.avatar);
                links.html.ShouldBeEquivalentTo(expectedLinks.html);
                links.self.ShouldBeEquivalentTo(expectedLinks.self);
            }

            return links;
        }
    }
}