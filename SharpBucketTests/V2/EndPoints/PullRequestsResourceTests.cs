using System.Net;
using NUnit.Framework;
using SharpBucket.V2;
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
        public void ListPullRequests_NotExistingRepository_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => SampleRepositories.NotExistingRepository.PullRequestsResource().ListPullRequests());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestLog_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = SampleRepositories.MercurialRepository.PullRequestsResource().GetPullRequestLog();
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count.ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void GetPullRequestLog_NotExistingRepository_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => SampleRepositories.NotExistingRepository.PullRequestsResource().GetPullRequestLog());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
