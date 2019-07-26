using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class CommentAssertions
    {
        public static Comment ShouldBeFilled(this Comment comment)
        {
            comment.ShouldNotBeNull();
            comment.links.ShouldBeFilled();
            comment.content.ShouldBeFilled();
            comment.created_on.ShouldNotBeNullOrEmpty();
            comment.user.ShouldBeFilled();
            comment.updated_on.ShouldNotBeNullOrEmpty();
            comment.id.ShouldNotBeNull();
            comment.deleted.ShouldNotBeNull();
            ////comment.parent is null if it's not a reply to a previous comment

            return comment;
        }
    }
}