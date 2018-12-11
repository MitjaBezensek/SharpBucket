using NUnit.Framework;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class PullRequestsResourceTests
    {
        [Test]
        public void ListPullRequests_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = SampleRepositories.MercurialRepository.PullRequestsResource().ListPullRequests();
            pullRequests.ShouldNotBeNull();

            // There is right now one open pull request on that repository.
            // Since it's a mirror it won't be accepted, but let's hope that nobody will come to decline it.
            // Otherwise we will have to perform our tests on another repository (maybe a private one that we can master, but we don't have it right now)
            pullRequests.Count.ShouldBeGreaterThanOrEqualTo(1);
        }

        [Test]
        public void ListPullRequests_NotExistingPublicPullRequest_ReturnEmpty()
        {
            var pullRequests = SampleRepositories.NotExistingRepository.PullRequestsResource().ListPullRequests();
            pullRequests.ShouldBeEmpty();
        }

        [Test]
        public void GetPullRequestLog_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = SampleRepositories.MercurialRepository.PullRequestsResource().GetPullRequestLog();
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count.ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void GetPullRequestLog_NotExistingPublicPullRequest_ReturnEmpty()
        {
            var pullRequestActivities = SampleRepositories.NotExistingRepository.PullRequestsResource().GetPullRequestLog();
            pullRequestActivities.ShouldBeEmpty();
        }
    }
}
