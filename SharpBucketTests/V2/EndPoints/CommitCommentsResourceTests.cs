using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class CommitCommentsResourceTests
        : CommentsResourceTests<CommitCommentsResource, CommitComment>
    {
        [Test]
        public void PostGetPutAndDeleteACommentOnACommit()
        {
            var commit = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit;
            var commitComments = SampleRepositories.TestRepository.RepositoryResource.CommitResource(commit).CommentsResource;

            PostGetPutAndDeleteAComment(commitComments);
            PostAnInlineComment(commitComments, new Location { path = "src/fileToChange.txt" });
        }
    }
}
