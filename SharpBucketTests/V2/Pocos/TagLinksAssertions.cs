using SharpBucket.V2.Pocos;

using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TagLinksAssertions
    {
        public static TagLinks ShouldBeFilled(this TagLinks tagLinks)
        {
            tagLinks.ShouldNotBeNull();
            tagLinks.self.ShouldBeFilled();
            tagLinks.html.ShouldBeFilled();
            tagLinks.commits.ShouldBeFilled();

            return tagLinks;
        }
    }
}
