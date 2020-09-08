using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class PullRequestsResourceTests
    {
        private PullRequestsResource ExistingRepository { get; set; }

        private int OpenedPullRequestId { get; set; }

        private int DeclinedPullRequestId { get; set; }

        private PullRequestsResource NotExistingRepository { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {

            ExistingRepository = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();

            // Getting those sample PRs ensure that there is at least 2 PRs with a known status on the test repo.
            // and a minimal set of pull request activities.
            OpenedPullRequestId = SampleOpenedPullRequest.Get.PullRequest.id.Value;
            DeclinedPullRequestId = SampleDeclinedPullRequest.Get.PullRequest.id.Value;

            NotExistingRepository = SampleRepositories.NotExistingRepository.PullRequestsResource();
        }

        [Test]
        public void ListPullRequests_ExistingRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = ExistingRepository.ListPullRequests();

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(false);
        }

        [Test]
        public void ListPullRequests_ExistingRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new ListPullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = ExistingRepository.ListPullRequests(parameters);

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(true);
        }

        [Test]
        public void ListPullRequests_NotExistingRepository_ThrowException()
        {
            var exception = Should.Throw<BitbucketV2Exception>(() => NotExistingRepository.ListPullRequests());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequests_ExistingRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = ExistingRepository.EnumeratePullRequests().ToList();

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(false);
        }

        [Test]
        public void EnumeratePullRequests_ExistingRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new EnumeratePullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = ExistingRepository.EnumeratePullRequests(parameters).ToList();

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(true);
        }

        [Test]
        public void EnumeratePullRequests_NotExistingRepository_ThrowException()
        {
            var lazyEnumeration = NotExistingRepository.EnumeratePullRequests();
            var exception = Should.Throw<BitbucketV2Exception>(() => lazyEnumeration.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestsAsync_ExistingRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequests = await ExistingRepository.EnumeratePullRequestsAsync().ToListAsync();

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(false);
        }

        [Test]
        public async Task EnumeratePullRequestsAsync_ExistingRepositoryWithPullRequest_AllStates_ReturnValidInfo()
        {
            var parameters = new EnumeratePullRequestsParameters
            {
                States = new[] { PullRequestState.Open, PullRequestState.Merged, PullRequestState.Declined, PullRequestState.Superseded }
            };
            var pullRequests = await ExistingRepository.EnumeratePullRequestsAsync(parameters).ToListAsync();

            pullRequests.Any(p => p.id == OpenedPullRequestId).ShouldBe(true);
            pullRequests.Any(p => p.id == DeclinedPullRequestId).ShouldBe(true);
        }

        [Test]
        public void EnumeratePullRequestsAsync_NotExistingRepository_ThrowException()
        {
            var lazyEnumeration = NotExistingRepository.EnumeratePullRequestsAsync();
            var exception = Should.Throw<BitbucketV2Exception>(async () => await lazyEnumeration.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestsActivities_ExistingRepositoryWithPullRequest_ReturnValidInfo()
        {
            var pullRequestActivities = ExistingRepository.GetPullRequestsActivities();
            pullRequestActivities.ShouldNotBeNull();
            pullRequestActivities.Count.ShouldBeGreaterThanOrEqualTo(6);
        }

        [Test]
        public void GetPullRequestsActivities_ExistingRepositoryWithPullRequest_WithMaxLimit_ReturnValidInfo()
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
        public void EnumeratePullRequestsActivities_ExistingRepositoryWithPullRequest_ReturnValidInfo()
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
        public async Task EnumeratePullRequestsActivitiesAsync_ExistingRepositoryWithPullRequest_ReturnValidInfo()
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
