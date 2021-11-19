using System.Collections.Generic;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class NamedLinkAssertions
    {
        public static NamedLink ShouldBeFilled(this NamedLink link)
        {
            link.ShouldNotBeNull();
            link.href.ShouldNotBeNullOrEmpty();
            link.name.ShouldNotBeNullOrEmpty();

            return link;
        }

        public static TEnumerableOfNamedLink ShouldAllBeFilled<TEnumerableOfNamedLink>(this TEnumerableOfNamedLink links)
            where TEnumerableOfNamedLink : class, IEnumerable<NamedLink>
        {
            links.ShouldNotBeNull();

            foreach (var namedLink in links)
            {
                namedLink.ShouldBeFilled();
            }

            return links;
        }
    }
}
