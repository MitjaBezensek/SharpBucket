using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class CommentsResourceTests
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
            var comments = ExistingPullRequest.Comments.List();
            comments.ShouldNotBeNull();
            comments.Count.ShouldBe(2);
            comments[0].ShouldBeFilled();
            comments[0].content.raw.ShouldBe("This repo is not used for development, it's just a mirror (and I am just an infrequent contributor). Please consult http://mercurial.selenic.com/wiki/ContributingChanges and send your patch to ``mercurial-devel`` ML.");
        }

        [Test]
        public void List_NotExistingPublicPullRequest_ThrowException()
        {
            var exception = Assert.Throws<BitbucketV2Exception>(() => NotExistingPullRequest.Comments.List());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public void Enumerate_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.Comments.Enumerate();
            comments.ShouldNotBeNull();
            comments.ShouldNotBeEmpty();
        }

        [Test]
        public void Enumerate_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.Comments.Enumerate();
            var exception = Assert.Throws<BitbucketV2Exception>(() => comments.First());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task EnumerateAsync_ExistingPublicPullRequest_ReturnValidInfo()
        {
            var comments = ExistingPullRequest.Comments.EnumerateAsync();
            comments.ShouldNotBeNull();
            (await comments.ToListAsync()).ShouldNotBeEmpty();
        }

        [Test]
        public void EnumerateAsync_NotExistingPublicPullRequest_ThrowExceptionWhenStartEnumerate()
        {
            var comments = NotExistingPullRequest.Comments.EnumerateAsync();
            var exception = Assert.ThrowsAsync<BitbucketV2Exception>(async () => await comments.FirstAsync());
            exception.HttpStatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
