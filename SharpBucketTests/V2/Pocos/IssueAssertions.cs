using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class IssueAssertions
    {
        public static void ShouldBeFilled(this Issue issue)
        {
            issue.ShouldNotBeNull();
            issue.content.ShouldNotBeNull();
            issue.links.ShouldNotBeNull();
            issue.reporter.ShouldBeFilled();
            issue.repository.ShouldNotBeNull();
        }
    }
}