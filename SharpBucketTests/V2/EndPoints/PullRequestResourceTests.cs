using System;
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
    internal class PullRequestResourceTests
    {
        private PullRequestResource ExistingPullRequest => SampleOpenedPullRequest.Get.PullRequestResource;

        private PullRequestResource NotExistingPullRequest { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();

            // there is no change that a pull request with the max value of int32 exists one day
            NotExistingPullRequest = pullRequestsResource.PullRequestResource(int.MaxValue);
        }

        [Test]
        public void GetPullRequest_ExistingPullRequest_ReturnValidInfo()
        {
            var pullRequest = SampleDeclinedPullRequest.Get.PullRequestResource.GetPullRequest();
            pullRequest.ShouldNotBeNull();
            pullRequest.id.ShouldNotBeNull();
            pullRequest.author.ShouldNotBeNull();
            pullRequest.title.ShouldNotBeNullOrEmpty();
            pullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public async Task GetPullRequestAsync_ExistingPullRequest_ReturnValidInfo()
        {
            var pullRequest = await SampleDeclinedPullRequest.Get.PullRequestResource.GetPullRequestAsync();
            pullRequest.ShouldNotBeNull();
            pullRequest.id.ShouldNotBeNull();
            pullRequest.author.ShouldNotBeNull();
            pullRequest.title.ShouldNotBeNullOrEmpty();
            pullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public void GetPullRequest_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.GetPullRequest());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestAsync_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await NotExistingPullRequest.GetPullRequestAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void ListPullRequestActivities_ExistingPullRequest_ReturnValidInfo()
        {
            var activities = SampleDeclinedPullRequest.Get.PullRequestResource.ListPullRequestActivities();
            activities.ShouldNotBeNull();
            activities.Count.ShouldBeGreaterThanOrEqualTo(1);
            activities[0].update.state.ShouldBe("DECLINED");
        }

        [Test]
        public void ListPullRequestActivities_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.ListPullRequestActivities());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestActivities_ExistingPullRequest_ReturnValidInfo()
        {
            var activities = ExistingPullRequest.EnumeratePullRequestActivities();
            activities.ShouldNotBeNull();
            activities.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestActivities_NotExistingPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var activities = NotExistingPullRequest.EnumeratePullRequestActivities();
            var exception = Assert.Throws<BitbucketV2Exception>(() => activities.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestActivitiesAsync_ExistingPullRequest_ReturnValidInfo()
        {
            var activities = ExistingPullRequest.EnumeratePullRequestActivitiesAsync();
            activities.ShouldNotBeNull();
            (await activities.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestActivitiesAsync_NotExistingPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var activities = NotExistingPullRequest.EnumeratePullRequestActivitiesAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await activities.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void ListPullRequestCommits_ExistingPullRequest_ReturnValidInfo()
        {
            var pullRequestResource = SampleDeclinedPullRequest.Get.PullRequestResource;
            var commits = pullRequestResource.ListPullRequestCommits();
            commits.ShouldNotBeNull();
            commits.Count.ShouldBeGreaterThanOrEqualTo(1);
            commits[0].message.ShouldStartWith("bad work that will be declined");
        }

        [Test]
        public void ListPullRequestCommits_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.ListPullRequestCommits());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestCommits_ExistingPullRequest_ReturnValidInfo()
        {
            var commits = ExistingPullRequest.EnumeratePullRequestCommits();
            commits.ShouldNotBeNull();
            commits.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestCommits_NotExistingPullRequest_ThrowExceptionWhenStartToEnumerate()
        {
            var commits = NotExistingPullRequest.EnumeratePullRequestCommits();
            var exception = Assert.Throws<BitbucketV2Exception>(() => commits.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestCommitsAsync_ExistingPullRequest_ReturnValidInfo()
        {
            var commits = ExistingPullRequest.EnumeratePullRequestCommitsAsync();
            commits.ShouldNotBeNull();
            (await commits.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestCommitsAsync_NotExistingPullRequest_ThrowExceptionWhenStartToEnumerate()
        {
            var commits = NotExistingPullRequest.EnumeratePullRequestCommitsAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await commits.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetDiffForPullRequest_ExistingPullRequest_ReturnValidInfo()
        {
            var diff = ExistingPullRequest.GetDiffForPullRequest();
            diff.ShouldNotBeNull();
            diff.ShouldStartWith("diff ");
        }

        [Test]
        public async Task GetDiffForPullRequestAsync_ExistingPullRequest_ReturnValidInfo()
        {
            var diff = await ExistingPullRequest.GetDiffForPullRequestAsync();
            diff.ShouldNotBeNull();
            diff.ShouldStartWith("diff ");
        }

        [Test]
        public void GetDiffForPullRequest_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.GetDiffForPullRequest());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetDiffForPullRequestAsync_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await NotExistingPullRequest.GetDiffForPullRequestAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void DeclinePullRequest_CreateAPullRequestThenDeclineIt_BranchStateShouldChangeFromOpenToDeclined()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();
            var pullRequestToDecline = new PullRequest
            {
                title = "a bad work",
                source = new Source { branch = new Branch { name = "branchToDecline" } }
            };
            var pullRequest = pullRequestsResource.PostPullRequest(pullRequestToDecline);
            pullRequest.state.ShouldBe("OPEN");

            var declinedPullRequest = pullRequestsResource.PullRequestResource(pullRequest.id.GetValueOrDefault()).DeclinePullRequest();
            declinedPullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public async Task DeclinePullRequestAsync_CreateAPullRequestAsyncThenDeclineItAsync_BranchStateShouldChangeFromOpenToDeclined()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();
            var pullRequestToDecline = new PullRequest
            {
                title = "a bad work",
                source = new Source { branch = new Branch { name = "branchToDecline" } }
            };
            var pullRequest = await pullRequestsResource.PostPullRequestAsync(pullRequestToDecline);
            pullRequest.state.ShouldBe("OPEN");

            var declinedPullRequest = await pullRequestsResource.PullRequestResource(pullRequest.id.GetValueOrDefault()).DeclinePullRequestAsync();
            declinedPullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public void ApprovePullRequestAndRemovePullRequestApproval_OnAnOpenedPullRequest_ActivityShouldFollowThatChanges()
        {
            // get the pull request
            var pullRequestResource = SampleOpenedPullRequest.Get.PullRequestResource;

            // approve the pull request
            var approvalResult = pullRequestResource.ApprovePullRequest();
            approvalResult.approved.ShouldBe(true);
            approvalResult.user.nickname.ShouldBe(TestHelpers.AccountName);
            approvalResult.role.ShouldBe("PARTICIPANT");

            // validate pull request activities after approval
            var activities = pullRequestResource.ListPullRequestActivities();
            activities.Count.ShouldBeGreaterThanOrEqualTo(2, "creation twice (for an unknown reason that may change) and approve");
            var approvalActivity = activities[0];
            approvalActivity.comment.ShouldBe(null);
            approvalActivity.update.ShouldBe(null);
            approvalActivity.pull_request.ShouldNotBeNull();
            approvalActivity.pull_request.title.ShouldBe("a good work to approve");
            approvalActivity.approval.ShouldNotBeNull();
            approvalActivity.approval.date.ShouldBe(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1));
            approvalActivity.approval.user.nickname.ShouldBe(TestHelpers.AccountName);

            // remove approval
            pullRequestResource.RemovePullRequestApproval();

            // validate pull request activities after having remove the approval
            var activitiesAfterRemoveApproval = pullRequestResource.ListPullRequestActivities();
            activitiesAfterRemoveApproval.Count.ShouldBe(activities.Count - 1, "Approval activity is removed, and removal is not traced.");
            activitiesAfterRemoveApproval.ShouldAllBe(activity => activity.approval == null, "should all be non approval activities");
        }

        [Test]
        public async Task ApprovePullRequestAsyncAndRemovePullRequestApprovalAsync_OnAnOpenedPullRequest_ActivityShouldFollowThatChanges()
        {
            // get the pull request
            var pullRequestResource = SampleOpenedPullRequest.Get.PullRequestResource;

            // approve the pull request
            var approvalResult = await pullRequestResource.ApprovePullRequestAsync();
            approvalResult.approved.ShouldBe(true);
            approvalResult.user.nickname.ShouldBe(TestHelpers.AccountName);
            approvalResult.role.ShouldBe("PARTICIPANT");

            // validate pull request activities after approval
            var activities = pullRequestResource.ListPullRequestActivities();
            activities.Count.ShouldBeGreaterThanOrEqualTo(2, "creation twice (for an unknown reason that may change) and approve");
            var approvalActivity = activities[0];
            approvalActivity.comment.ShouldBe(null);
            approvalActivity.update.ShouldBe(null);
            approvalActivity.pull_request.ShouldNotBeNull();
            approvalActivity.pull_request.title.ShouldBe("a good work to approve");
            approvalActivity.approval.ShouldNotBeNull();
            approvalActivity.approval.date.ShouldBe(DateTimeOffset.UtcNow, TimeSpan.FromMinutes(1));
            approvalActivity.approval.user.nickname.ShouldBe(TestHelpers.AccountName);

            // remove approval
            await pullRequestResource.RemovePullRequestApprovalAsync();

            // validate pull request activities after having remove the approval
            var activitiesAfterRemoveApproval = pullRequestResource.ListPullRequestActivities();
            activitiesAfterRemoveApproval.Count.ShouldBe(activities.Count - 1, "Approval activity is removed, and removal is not traced.");
            activitiesAfterRemoveApproval.ShouldAllBe(activity => activity.approval == null, "should all be non-approved activities");
        }
    }
}
