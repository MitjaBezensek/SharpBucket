using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class RepositoryInfoLinksAssertions
    {
        public static RepositoryInfoLinks ShouldBeFilled(this RepositoryInfoLinks links)
        {
            links.ShouldNotBeNull();
            links.avatar.ShouldBeFilled();
            links.html.ShouldBeFilled();
            links.self.ShouldBeFilled();

            return links;
        }
    }
}