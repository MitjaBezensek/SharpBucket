using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class PullRequestsResourceTests
    {
        private PullRequestsResource ExistingRepository { get; set; }

        private PullRequestsResource NotExistingRepository { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            // There is right now one open pull request on that repository.
            // Since it's a mirror it won't be accepted, but let's hope that nobody will come to decline it.
            // Otherwise we will have to perform our tests on another repository (a private one that we can master).
            ExistingRepository = SampleRepositories.MercurialRepository.PullRequestsResource();

            NotExistingRepository = SampleRepositories.NotExistingRepository.PullRequestsResource();
        }

        [Test]
        public void ListPullRequests_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = ExistingRepository.ListPullRequests();

            pullRequests.ShouldNotBeNull();
            pullRequests.Count().ShouldBe(1, "Only one opened pull request is known on Mercurial repository");
        }

        [Test]
        public void ListPullRequests_ExistingPublicRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new ListPullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = ExistingRepository.ListPullRequests(parameters);
            pullRequests.ShouldNotBeNull();
            pullRequests.Count().ShouldBeGreaterThan(1, "When we don't limit to open pull request we can found more!");
        }

        [Test]
        public void ListPullRequests_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(() => NotExistingRepository.ListPullRequests());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequests_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = ExistingRepository.EnumeratePullRequests();
            pullRequests.ShouldNotBeNull();
            pullRequests.Count().ShouldBe(1, "Only one opened pull request is known on Mercurial repository");
        }

        [Test]
        public void EnumeratePullRequests_ExistingPublicRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new EnumeratePullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = ExistingRepository.EnumeratePullRequests(parameters);
            pullRequests.ShouldNotBeNull();
            pullRequests.Count().ShouldBeGreaterThan(1, "When we don't limit to open pull request we can found more!");
        }

        [Test]
        public void EnumeratePullRequests_NotExistingRepository_ThrowException()
        {
            var lazyEnumeration = NotExistingRepository.EnumeratePullRequests();
            var exception = Should.Throw<BitbucketV2Exception>(() => lazyEnumeration.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestsAsync_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = ExistingRepository.EnumeratePullRequestsAsync();
            pullRequests.ShouldNotBeNull();
            (await pullRequests.ToListAsync()).Count
                .ShouldBe(1, "Only one opened pull request is known on Mercurial repository");
        }

        [Test]
        public async Task EnumeratePullRequestsAsync_ExistingPublicRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new EnumeratePullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = ExistingRepository.EnumeratePullRequestsAsync(parameters);
            pullRequests.ShouldNotBeNull();
            (await pullRequests.ToListAsync()).Count
                .ShouldBeGreaterThan(1, "When we don't limit to open pull request we can found more!");
        }

        [Test]
        public void EnumeratePullRequestsAsync_NotExistingRepository_ThrowException()
        {
            var lazyEnumeration = NotExistingRepository.EnumeratePullRequestsAsync();
            var exception = Should.Throw<BitbucketV2Exception>(async () => await lazyEnumeration.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestsActivities_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = ExistingRepository.GetPullRequestsActivities();
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count.ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void GetPullRequestsActivities_ExistingPublicRepositoryWithPullRequest_WithMaxLimit_ReturnValidInfo()
        {
            var pullRequestActivities = ExistingRepository.GetPullRequestsActivities(3);
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count.ShouldBe(3);
        }

        [Test]
        public void GetPullRequestsActivities_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(() => NotExistingRepository.GetPullRequestsActivities());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestsActivities_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = ExistingRepository.EnumeratePullRequestsActivities();
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count().ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void EnumeratePullRequestsActivities_NotExistingRepository_ThrowException()
        {
            var pullRequestActivities = NotExistingRepository.EnumeratePullRequestsActivities();
            var exception = Should.Throw<BitbucketV2Exception>(() => pullRequestActivities.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestsActivitiesAsync_ExistingPublicRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = ExistingRepository.EnumeratePullRequestsActivitiesAsync();
            pullRequestActivities.ShouldNotBeNull();
            (await pullRequestActivities.CountAsync()).ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void EnumeratePullRequestsActivitiesAsync_NotExistingRepository_ThrowException()
        {
            var pullRequestActivities = NotExistingRepository.EnumeratePullRequestsActivitiesAsync();
            var exception = Should.Throw<BitbucketV2Exception>(async () => await pullRequestActivities.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
