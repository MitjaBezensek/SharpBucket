using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class CommentResourceTests
    {
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
    }
}
