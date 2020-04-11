using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    internal class PullRequestResourceTests
    {
        private PullRequestResource ExistingPullRequest { get; set; }

        private PullRequestResource NotExistingPullRequest { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            // pull request number 2 on MercurialRepository is public and declined
            // so we could expect that it will be always accessible and won't change
            // which is what we need to have reproducible tests
            ExistingPullRequest = SampleRepositories.MercurialRepository.PullRequestsResource().PullRequestResource(2);

            // there is no change that a pull request with the max value of int32 exist one day
            NotExistingPullRequest = SampleRepositories.MercurialRepository.PullRequestsResource().PullRequestResource(int.MaxValue);
        }

        [Test]
        public void GetPullRequest_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var pullRequest = ExistingPullRequest.GetPullRequest();
            pullRequest.ShouldNotBeNull();
            pullRequest.id.ShouldBe(2);
            pullRequest.author?.nickname.ShouldBe("goodtune");
            pullRequest.title.ShouldBe("Selective read/write or read-only repos with hg-ssh");
            pullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public async Task GetPullRequestAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var pullRequest = await ExistingPullRequest.GetPullRequestAsync();
            pullRequest.ShouldNotBeNull();
            pullRequest.id.ShouldBe(2);
            pullRequest.author?.nickname.ShouldBe("goodtune");
            pullRequest.title.ShouldBe("Selective read/write or read-only repos with hg-ssh");
            pullRequest.state.ShouldBe("DECLINED");
        }

        [Test]
        public void GetPullRequest_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.GetPullRequest());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestAsync_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await NotExistingPullRequest.GetPullRequestAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void ListPullRequestActivities_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var activities = ExistingPullRequest.ListPullRequestActivities();
            activities.ShouldNotBeNull();
            activities.Count.ShouldBe(4);
            activities[0].update.state.ShouldBe("DECLINED");
        }

        [Test]
        public void ListPullRequestActivities_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.ListPullRequestActivities());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestActivities_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var activities = ExistingPullRequest.EnumeratePullRequestActivities();
            activities.ShouldNotBeNull();
            activities.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestActivities_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var activities = NotExistingPullRequest.EnumeratePullRequestActivities();
            var exception = Assert.Throws<BitbucketV2Exception>(() => activities.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestActivitiesAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var activities = ExistingPullRequest.EnumeratePullRequestActivitiesAsync();
            activities.ShouldNotBeNull();
            (await activities.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestActivitiesAsync_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var activities = NotExistingPullRequest.EnumeratePullRequestActivitiesAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await activities.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void ListPullRequestComments_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.ListPullRequestComments();
            comments.ShouldNotBeNull();
            comments.Count.ShouldBe(2);
            comments[0].ShouldBeFilled();
            comments[0].content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void ListPullRequestComments_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.ListPullRequestComments());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestComments_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.EnumeratePullRequestComments();
            comments.ShouldNotBeNull();
            comments.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestComments_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.EnumeratePullRequestComments();
            var exception = Assert.Throws<BitbucketV2Exception>(() => comments.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestCommentsAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.EnumeratePullRequestCommentsAsync();
            comments.ShouldNotBeNull();
            (await comments.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestCommentsAsync_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.EnumeratePullRequestCommentsAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await comments.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestComment_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = ExistingPullRequest.GetPullRequestComment(53789);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public async Task GetPullRequestCommentAsync_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await ExistingPullRequest.GetPullRequestCommentAsync(53789);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void GetPullRequestComment_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = SampleRepositories.RepositoriesEndPoint
                .RepositoryResource("tortoisehg", "thg")
                .PullRequestsResource()
                .PullRequestResource(46)
                .GetPullRequestComment(61843122);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public async Task GetPullRequestCommentAsync_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await SampleRepositories.RepositoriesEndPoint
                .RepositoryResource("tortoisehg", "thg")
                .PullRequestsResource()
                .PullRequestResource(46)
                .GetPullRequestCommentAsync(61843122);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public void GetPullRequestComment_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => ExistingPullRequest.GetPullRequestComment(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetPullRequestCommentAsync_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async() => await ExistingPullRequest.GetPullRequestCommentAsync(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void ListPullRequestCommits_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var commits = ExistingPullRequest.ListPullRequestCommits();
            commits.ShouldNotBeNull();
            commits.Count.ShouldBe(2);
            commits[0].message.ShouldBe("Update the docstring");
        }

        [Test]
        public void ListPullRequestCommits_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.ListPullRequestCommits());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void EnumeratePullRequestCommits_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var commits = ExistingPullRequest.EnumeratePullRequestCommits();
            commits.ShouldNotBeNull();
            commits.ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestCommits_NotExistingPublicPullRequest_ThrowExceptionWhenStartToEnumerate()
        {
            var commits = NotExistingPullRequest.EnumeratePullRequestCommits();
            var exception = Assert.Throws<BitbucketV2Exception>(() => commits.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumeratePullRequestCommitsAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var commits = ExistingPullRequest.EnumeratePullRequestCommitsAsync();
            commits.ShouldNotBeNull();
            (await commits.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumeratePullRequestCommitsAsync_NotExistingPublicPullRequest_ThrowExceptionWhenStartToEnumerate()
        {
            var commits = NotExistingPullRequest.EnumeratePullRequestCommitsAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await commits.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetDiffForPullRequest_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var diff = ExistingPullRequest.GetDiffForPullRequest();
            diff.ShouldNotBeNull();
            diff.ShouldBe("diff -r 0fbcabe523bc -r 4f9cfa6003cb contrib/hg-ssh\n--- a/contrib/hg-ssh\n+++ b/contrib/hg-ssh\n@@ -25,8 +25,8 @@\n You can use pattern matching of your normal shell, e.g.:\n command=\"cd repos && hg-ssh user/thomas/* projects/{mercurial,foo}\"\n \n-You can also add a --read-only flag to allow read-only access to a key, e.g.:\n-command=\"hg-ssh --read-only repos/*\"\n+You can also add a --read-only flag to allow read-only access to a repo, e.g.:\n+command=\"hg-ssh repo1 --read-only=repo2 repo3\"\n \"\"\"\n \n # enable importing on demand to reduce startup time\n@@ -34,21 +34,16 @@\n \n from mercurial import dispatch\n \n-import sys, os, shlex\n+import sys, optparse, os, shlex\n \n def main():\n+    parser = optparse.OptionParser()\n+    parser.add_option('--read-only', action='append', default=[])\n+    options, args = parser.parse_args()\n     cwd = os.getcwd()\n-    readonly = False\n-    args = sys.argv[1:]\n-    while len(args):\n-        if args[0] == '--read-only':\n-            readonly = True\n-            args.pop(0)\n-        else:\n-            break\n     allowed_paths = [os.path.normpath(os.path.join(cwd,\n                                                    os.path.expanduser(path)))\n-                     for path in args]\n+                     for path in args + options.read_only]\n     orig_cmd = os.getenv('SSH_ORIGINAL_COMMAND', '?')\n     try:\n         cmdargv = shlex.split(orig_cmd)\n@@ -61,7 +56,7 @@\n         repo = os.path.normpath(os.path.join(cwd, os.path.expanduser(path)))\n         if repo in allowed_paths:\n             cmd = ['-R', repo, 'serve', '--stdio']\n-            if readonly:\n+            if path in options.read_only:\n                 cmd += [\n                     '--config',\n                     'hooks.prechangegroup.hg-ssh=python:__main__.rejectpush',\n");
        }

        [Test]
        public async Task GetDiffForPullRequestAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var diff = await ExistingPullRequest.GetDiffForPullRequestAsync();
            diff.ShouldNotBeNull();
            diff.ShouldBe("diff -r 0fbcabe523bc -r 4f9cfa6003cb contrib/hg-ssh\n--- a/contrib/hg-ssh\n+++ b/contrib/hg-ssh\n@@ -25,8 +25,8 @@\n You can use pattern matching of your normal shell, e.g.:\n command=\"cd repos && hg-ssh user/thomas/* projects/{mercurial,foo}\"\n \n-You can also add a --read-only flag to allow read-only access to a key, e.g.:\n-command=\"hg-ssh --read-only repos/*\"\n+You can also add a --read-only flag to allow read-only access to a repo, e.g.:\n+command=\"hg-ssh repo1 --read-only=repo2 repo3\"\n \"\"\"\n \n # enable importing on demand to reduce startup time\n@@ -34,21 +34,16 @@\n \n from mercurial import dispatch\n \n-import sys, os, shlex\n+import sys, optparse, os, shlex\n \n def main():\n+    parser = optparse.OptionParser()\n+    parser.add_option('--read-only', action='append', default=[])\n+    options, args = parser.parse_args()\n     cwd = os.getcwd()\n-    readonly = False\n-    args = sys.argv[1:]\n-    while len(args):\n-        if args[0] == '--read-only':\n-            readonly = True\n-            args.pop(0)\n-        else:\n-            break\n     allowed_paths = [os.path.normpath(os.path.join(cwd,\n                                                    os.path.expanduser(path)))\n-                     for path in args]\n+                     for path in args + options.read_only]\n     orig_cmd = os.getenv('SSH_ORIGINAL_COMMAND', '?')\n     try:\n         cmdargv = shlex.split(orig_cmd)\n@@ -61,7 +56,7 @@\n         repo = os.path.normpath(os.path.join(cwd, os.path.expanduser(path)))\n         if repo in allowed_paths:\n             cmd = ['-R', repo, 'serve', '--stdio']\n-            if readonly:\n+            if path in options.read_only:\n                 cmd += [\n                     '--config',\n                     'hooks.prechangegroup.hg-ssh=python:__main__.rejectpush',\n");
        }

        [Test]
        public void GetDiffForPullRequest_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.GetDiffForPullRequest());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetDiffForPullRequestAsync_NotExistingPublicPullRequest_ThrowException()
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
        public void ApprovePullRequestAndRemovePullRequestApproval_CreateAPullRequestThenChangeMyApproval_ActivityShouldFollowThatChanges()
        {
            // create the pull request
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();
            var pullRequestToApprove = new PullRequest
            {
                title = "a good work to approve",
                source = new Source { branch = new Branch { name = "branchToAccept" } }
            };
            var pullRequest = pullRequestsResource.PostPullRequest(pullRequestToApprove);
            var pullRequestResource = pullRequestsResource.PullRequestResource(pullRequest.id.GetValueOrDefault());

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
            activitiesAfterRemoveApproval.ShouldAllBe(activity => activity.update != null, "should all be update activities");
        }

        [Test]
        public async Task ApprovePullRequestAsyncAndRemovePullRequestApprovalAsync_CreateAPullRequestAsyncThenChangeMyApprovalAsync_ActivityShouldFollowThatChanges()
        {
            // create the pull request
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();
            var pullRequestToApprove = new PullRequest
            {
                title = "a good work to approve",
                source = new Source { branch = new Branch { name = "branchToAccept" } }
            };
            var pullRequest = await pullRequestsResource.PostPullRequestAsync(pullRequestToApprove);
            var pullRequestResource = pullRequestsResource.PullRequestResource(pullRequest.id.GetValueOrDefault());

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
            activitiesAfterRemoveApproval.ShouldAllBe(activity => activity.update != null, "should all be update activities");
        }
    }
}
