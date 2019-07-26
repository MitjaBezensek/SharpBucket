using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class RenderedAssertions
    {
        public static Rendered ShouldBeFilled(this Rendered rendered)
        {
            rendered.ShouldNotBeNull();
            rendered.html.ShouldNotBeNullOrEmpty();
            rendered.markup.ShouldNotBeNullOrEmpty();
            rendered.raw.ShouldNotBeNullOrEmpty();
            rendered.type.ShouldBe("rendered");

            return rendered;
        }
    }
}