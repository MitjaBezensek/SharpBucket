
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    public abstract class CommentsResourceTests<TCommentsResource, TComment>
        where TCommentsResource : CommentsResource<TComment>
        where TComment : Comment, new()
    {
        protected static void PostGetPutAndDeleteAComment(TCommentsResource commentsResource)
        {
            var originalComment = commentsResource.PostComment("A global comment on a commit");
            originalComment.ShouldBeFilled().And().ShouldBeAGlobalComment();

            var originalCommentId = originalComment.id.GetValueOrDefault();
            var getComment = commentsResource.GetComment(originalCommentId);
            getComment.ShouldBeFilled().And().ShouldBeAGlobalComment();

            getComment.content.raw = "Altered comment";
            var putComment = commentsResource.PutComment(getComment);
            putComment.ShouldBeFilled().And().content.raw.ShouldBe("Altered comment");

            PostAReplyOnAComment(commentsResource, getComment);

            commentsResource.DeleteComment(originalCommentId);
        }

        protected static void PostAnInlineComment(TCommentsResource commentsResource, Location location)
        {
            var inlineComment = commentsResource.PostComment("this is an inline comment", location);
            inlineComment.ShouldBeFilled().And().ShouldBeAnInlineComment();

            PostAReplyOnAComment(commentsResource, inlineComment);
        }

        protected static void PostAReplyOnAComment(TCommentsResource commentsResource, TComment parent)
        {
            var replyComment = commentsResource.PostComment("this is a reply to " + parent.content.raw, parent.id);
            replyComment.ShouldBeFilled().And().ShouldBeAReplyComment();
        }
    }
}
