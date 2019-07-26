using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class RepositoryLinksAssertions
    {
        public static RepositoryLinks ShouldBeFilled(this RepositoryLinks links)
        {
            links.ShouldNotBeNull();
            links.avatar.ShouldBeFilled();
            links.branches.ShouldBeFilled();
            links.clone.ShouldAllBeFilled();
            links.commits.ShouldBeFilled();
            links.downloads.ShouldBeFilled();
            links.forks.ShouldBeFilled();
            links.hooks.ShouldBeFilled();
            links.html.ShouldBeFilled();
            links.pullrequests.ShouldBeFilled();
            links.self.ShouldBeFilled();
            links.source.ShouldBeFilled();
            links.tags.ShouldBeFilled();
            links.watchers.ShouldBeFilled();

            return links;
        }
    }
}