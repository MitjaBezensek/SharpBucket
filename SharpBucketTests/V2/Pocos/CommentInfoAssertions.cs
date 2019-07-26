using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommentInfoAssertions
    {
        public static CommentInfo ShouldBeFilled(this CommentInfo comment)
        {
            comment.ShouldNotBeNull();
            comment.links.ShouldBeFilled();
            comment.id.ShouldNotBeNull();

            return comment;
        }
    }
}