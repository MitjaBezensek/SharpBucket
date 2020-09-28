using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommitAssertions
    {
        public static Commit ShouldBeFilled(this Commit commit)
        {
            commit.ShouldNotBeNull();
            commit.author.ShouldBeFilled();
            commit.date.ShouldNotBeNullOrEmpty();
            commit.hash.ShouldNotBeNull();
            commit.message.ShouldNotBeNullOrEmpty();
            ////commit.parents could be null for initial commit
            ////commit.repository.ShouldBeFilled(); at least in Branch POCO it seems that this should be just a RepositoryInfo. TODO must verify in other contexts before changing type.

            return commit;
        }
    }
}
