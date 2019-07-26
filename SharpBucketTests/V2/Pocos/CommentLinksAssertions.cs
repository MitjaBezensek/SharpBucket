using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommentLinksAssertions
    {
        public static CommentLinks ShouldBeFilled(this CommentLinks links)
        {
            links.ShouldNotBeNull();
            links.html.ShouldBeFilled();
            links.self.ShouldBeFilled();

            return links;
        }
    }
}