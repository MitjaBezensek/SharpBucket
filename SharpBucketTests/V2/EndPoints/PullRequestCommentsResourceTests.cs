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
        public void List_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.CommentsResource.ListComments();
            comments.ShouldNotBeNull();
            comments.Count.ShouldBe(2);
            comments[0].ShouldBeFilled();
            comments[0].content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void List_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.CommentsResource.ListComments());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void Enumerate_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.CommentsResource.EnumerateComments();
            comments.ShouldNotBeNull();
            comments.ShouldNotBeEmpty();
        }

        [Test]
        public void Enumerate_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.CommentsResource.EnumerateComments();
            var exception = Assert.Throws<BitbucketV2Exception>(() => comments.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumerateAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.CommentsResource.EnumerateCommentsAsync();
            comments.ShouldNotBeNull();
            (await comments.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateAsync_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
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
            var pullRequestResource = pullRequestsResource.PullRequestResource(pullRequest.id.Value);
            var pullRequestComments = pullRequestResource.CommentsResource;

            PostGetPutAndDeleteAComment(pullRequestComments);
            PostAnInlineComment(pullRequestComments, new Location { path = "badNewWork.txt" });

            pullRequestResource.DeclinePullRequest();
        }

        [Test]
        public void Get_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = ExistingPullRequest.CommentsResource.GetComment(53789);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public async Task GetAsync_ExistingCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await ExistingPullRequest.CommentsResource.GetCommentAsync(53789);
            comment.ShouldBeFilled();
            comment.content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void Get_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = SampleRepositories.TortoisehgRepository
                .PullRequestsResource()
                .PullRequestResource(46)
                .CommentsResource
                .GetComment(61843122);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public async Task GetAsync_ExistingReplyCommentOnAPublicPullRequest_ReturnValidInfo()
        {
            var comment = await SampleRepositories.TortoisehgRepository
                .PullRequestsResource()
                .PullRequestResource(46)
                .CommentsResource
                .GetCommentAsync(61843122);
            comment.ShouldBeFilled();
            comment.parent.ShouldBeFilled();
        }

        [Test]
        public void Get_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(
                () => ExistingPullRequest.CommentsResource.GetComment(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void GetAsync_NotExistingCommentOnPublicPullRequest_ThrowException()
        {
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(
                async () => await ExistingPullRequest.CommentsResource.GetCommentAsync(int.MaxValue));
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
