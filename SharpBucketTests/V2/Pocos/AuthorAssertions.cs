using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class AuthorAssertions
    {
        public static Author ShouldBeFilled(this Author author)
        {
            author.ShouldNotBeNull();
            author.raw.ShouldNotBeNullOrEmpty();
            ////author.user is null if raw do not match a known user of bitbucket

            return author;
        }
    }
}