using System;
using System.Diagnostics;
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
    public class PullRequestCommentsResourceTests
        : CommentsResourceTests<PullRequestCommentsResource, PullRequestComment>
    {
        private PullRequestResource ExistingPullRequestWithComments => SampleOpenedPullRequest.Get.PullRequestResource;

        private int PullRequestGlobalCommentId => SampleOpenedPullRequest.Get.GlobalComment.id
                                                  ?? throw new InvalidOperationException("that id is not expected to be null!");

        private int PullRequestResponseCommentId => SampleOpenedPullRequest.Get.ResponseComment.id
                                                    ?? throw new InvalidOperationException("that id is not expected to be null!");

        private PullRequestResource NotExistingPullRequest { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            // there is no change that a pull request with the max value of int32 exist one day
            NotExistingPullRequest = SampleRepositories.TestRepository.RepositoryResource
                .PullRequestsResource().PullRequestResource(int.MaxValue);
        }

        [Test]
        public void List_ExistingPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequestWithComments.CommentsResource.ListComments();
            comments.ShouldNotBeNull();
            comments.Count.ShouldBe(2);
            comments[0].ShouldBeFilled();
            comments[0].content.raw.ShouldBe("This PR is just for testing purposes.");
        }

        [Test]
        public void List_NotExistingPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.CommentsResource.ListComments());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void Enumerate_ExistingPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequestWithComments.CommentsResource.EnumerateComments();
            comments.ShouldNotBeNull();
            comments.ShouldNotBeEmpty();
        }

        [Test]
        public void Enumerate_NotExistingPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.CommentsResource.EnumerateComments();
            var exception = Assert.Throws<BitbucketV2Exception>(() => comments.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumerateAsync_ExistingPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequestWithComments.CommentsResource.EnumerateCommentsAsync();
            comments.ShouldNotBeNull();
            (await comments.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateAsync_NotExistingPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.CommentsResource.EnumerateCommentsAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await comments.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void PostGetPutAndDeleteACommentOnAPullRequest()
        {
            var pullRequestsResource = SampleRepositories.TestRepository.RepositoryResource.PullRequestsResource();
            var pullRequest = new PullRequest
            {
                title = "A work in progress",
                source = new Source { branch = new Branch { name = "branchToDecline" } }
            };
            pullRequest = pullRequestsResource.PostPullRequest(pullRequest);
            Debug.Assert(pullRequest.id != null, "pullRequest.id != null");
            var pullRequestResource = pullRequestsResource.PullRequestResource(pullRequest.id.Value);
            var pullRequestComments = pullRequestResource.CommentsResource;

            PostGetPutAndDeleteAComment(pullRequestComments);
            PostAnInlineComment(pullRequestComments, new Location { path = "badNewWork.txt" });

            pullRequestResource.DeclinePullRequest();
        }

        [Test]
        public void Get_ExistingCommentOnAPullRequest_ReturnValidInfo()
        {
            var comment = ExistingPullRequestWithComments.CommentsResource.GetComment(PullRequestGlobalCommentId);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This PR is just for testing purposes.");
        }

        [Test]
        public async Task GetAsync_ExistingCommentOnAPullRequest_ReturnValidInfo()
        {
            var comment = await ExistingPullRequestWithComments.CommentsResource.GetCommentAsync(PullRequestGlobalCommentId);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This PR is just for testing purposes.");
        }

        [Test]
        public void Get_ExistingReplyCommentOnAPullRequest_ReturnValidInfo()
        {
            var comment = ExistingPullRequestWithComments.CommentsResource.GetComment(PullRequestResponseCommentId);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public async Task GetAsync_ExistingReplyCommentOnAPullRequest_ReturnValidInfo()
        {
            var comment = await ExistingPullRequestWithComments.CommentsResource.GetCommentAsync(PullRequestResponseCommentId);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public void Get_NotExistingCommentOnPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(
                () => ExistingPullRequestWithComments.CommentsResource.GetComment(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetAsync_NotExistingCommentOnPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(
                async () => await ExistingPullRequestWithComments.CommentsResource.GetCommentAsync(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
