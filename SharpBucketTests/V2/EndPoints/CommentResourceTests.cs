using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

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

            pullRequestResource.DeclinePullRequest();
        }

        private void PostGetPutAndDeleteAComment(CommentsResource commentsResource)
        {
            var originalComment = commentsResource.Post("A global comment on a commit");
            var commentResource = commentsResource.Comment(originalComment.id.GetValueOrDefault());
            var getComment = commentResource.Get();
            getComment.content.raw = "Altered comment";
            commentResource.Put(getComment);
            commentResource.Delete();
        }
    }
}
