using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class LinkAssertions
    {
        public static Link ShouldBeFilled(this Link link)
        {
            link.ShouldNotBeNull();
            link.href.ShouldNotBeNullOrEmpty();

            return link;
        }

        public static Link ShouldBeEquivalentTo(this Link link, Link expectedLink)
        {
            if (expectedLink == null)
            {
                link.ShouldBeNull();
            }
            else
            {
                link.ShouldNotBeNull();
                link.href.ShouldBe(expectedLink.href);
            }

            return link;
        }
    }
}
