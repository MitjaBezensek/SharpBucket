using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TagAssertions
    {
        public static Tag ShouldBeFilled(this Tag tag)
        {
            tag.ShouldNotBeNull();
            tag.name.ShouldNotBeNullOrEmpty();
            tag.tagger.ShouldNotBeNull();
            tag.date.ShouldNotBeNull();
            tag.message.ShouldNotBeNull();

            return tag;
        }
    }
}
