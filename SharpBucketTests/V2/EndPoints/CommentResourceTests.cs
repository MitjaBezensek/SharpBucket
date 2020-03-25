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
    public class CommentResourceTests
    {
        private PullRequestResource ExistingPullRequest { get; set; }

        [OneTimeSetUp]
        protected void Init()
        {
            // pull request number 2 on MercurialRepository is public and declined
            // so we could expect that it will be always accessible and won't change
            // which is what we need to have reproducible tests
            ExistingPullRequest = SampleRepositories.MercurialRepository.PullRequestsResource().PullRequestResource(2);
        }

        [Test]
        public void PostGetPutAndDeleteACommentOnACommit()
        {
            var commit = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit;
            var commitComments = SampleRepositories.TestRepository.RepositoryResource.Commit(commit).Comments;

            PostGetPutAndDeleteAComment(commitComments);
            PostAnInlineComment(commitComments, new Location { path = "src/fileToChange.txt" });
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
            var pullRequestResource = pullRequestsResource.PullRequestResource(pullRequest.id.Value);
            var pulRequestComments = pullRequestResource.Comments;

            PostGetPutAndDeleteAComment(pulRequestComments);
            PostAnInlineComment(pulRequestComments, new Location { path = "badNewWork.txt" });

            pullRequestResource.DeclinePullRequest();
        }

        private void PostGetPutAndDeleteAComment(CommentsResource commentsResource)
        {
            var originalComment = commentsResource.Post("A global comment on a commit");
            originalComment.ShouldBeFilled().And().ShouldBeAGlobalComment();

            var commentResource = commentsResource.Comment(originalComment.id.GetValueOrDefault());
            var getComment = commentResource.Get();
            getComment.ShouldBeFilled().And().ShouldBeAGlobalComment();

            getComment.content.raw = "Altered comment";
            var putComment = commentResource.Put(getComment);
            putComment.ShouldBeFilled().And().content.raw.ShouldBe("Altered comment");

            this.PostAReplyOnAComment(commentsResource, getComment);

            commentResource.Delete();
        }

        private void PostAReplyOnAComment(CommentsResource commentsResource, Comment parent)
        {
            var replyComment = commentsResource.Post("this is a reply to " + parent.content.raw, parent.id);
            replyComment.ShouldBeFilled().And().ShouldBeAReplyComment();
        }

        private void PostAnInlineComment(CommentsResource commentsResource, Location location)
        {
            var inlineComment = commentsResource.Post("this is an inline comment", location);
            inlineComment.ShouldBeFilled().And().ShouldBeAnInlineComment();

            PostAReplyOnAComment(commentsResource, inlineComment);
        }

        [Test]
        public void Get_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = ExistingPullRequest.Comments.Comment(53789).Get();
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public async Task GetAsync_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await ExistingPullRequest.Comments.Comment(53789).GetAsync();
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void Get_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = SampleRepositories.RepositoriesEndPoint
                .PullRequestsResource("tortoisehg", "thg")
                .PullRequestResource(46)
                .Comments
                .Comment(61843122)
                .Get();
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public async Task GetAsync_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await SampleRepositories.RepositoriesEndPoint
                .PullRequestsResource("tortoisehg", "thg")
                .PullRequestResource(46)
                .Comments
                .Comment(61843122)
                .GetAsync();
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public void Get_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(
                () => ExistingPullRequest.Comments.Comment(int.MaxValue).Get());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetAsync_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(
                async () => await ExistingPullRequest.Comments.Comment(int.MaxValue).GetAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
