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
            tag.target.ShouldBeFilled();
            tag.tagger.ShouldBeFilled();
            tag.date.ShouldNotBeNullOrEmpty();
            tag.message.ShouldNotBeNull();
            tag.links.ShouldBeFilled();

            return tag;
        }

        public static Tag ShouldBeEquivalentTo(this Tag tag, Tag expectedTag)
        {
            if (expectedTag == null)
            {
                tag.ShouldBeNull();
            }
            else
            {
                tag.ShouldBeFilled();
                tag.name.ShouldBe(expectedTag.name);
                tag.message.ShouldBe(expectedTag.message);
                tag.target.hash.ShouldBe(expectedTag.target.hash);
            }

            return tag;
        }
    }
}
