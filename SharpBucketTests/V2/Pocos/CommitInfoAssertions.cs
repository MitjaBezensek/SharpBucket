using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommitInfoAssertions
    {
        public static CommitInfo ShouldBeFilled(this CommitInfo commitInfo)
        {
            commitInfo.ShouldNotBeNull();
            commitInfo.hash.ShouldNotBeNull();
            commitInfo.links.ShouldBeFilled();

            return commitInfo;
        }

        public static CommitInfo ShouldBeAReferenceTo(this CommitInfo commitInfo, string hash)
        {
            commitInfo.hash.ShouldBe(hash);

            return commitInfo;
        }
    }
}
