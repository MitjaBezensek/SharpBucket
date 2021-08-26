using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bnode%7D/comments
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bnode%7D/comments/%7Bcomment_id%7D
    /// or
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments/%7Bcomment_id%7D#put
    /// </summary>
    public abstract class CommentsResource<TComment> : EndPoint
        where TComment : Comment, new()
    {
        protected CommentsResource(EndPoint parentResource)
            : base(parentResource, "comments")
        {
        }

        /// <summary>
        /// List comments associated with this resource.
        /// </summary>
        public List<TComment> ListComments()
            => ListComments(new ListParameters());

        /// <summary>
        /// List comments associated with this resource.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<TComment> ListComments(ListParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return SharpBucketV2.GetPaginatedValues<TComment>(
                BaseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing requests page by page while enumerating.
        /// </summary>
        public IEnumerable<TComment> EnumerateComments()
            => EnumerateComments(new EnumerateParameters());

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<TComment> EnumerateComments(EnumerateParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return SharpBucketV2.EnumeratePaginatedValues<TComment>(
                BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing async requests page by page while enumerating.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<TComment> EnumerateCommentsAsync(CancellationToken token = default)
            => EnumerateCommentsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing async requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<TComment> EnumerateCommentsAsync(
            EnumerateParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return SharpBucketV2.EnumeratePaginatedValuesAsync<TComment>(
                BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        public TComment PostComment(string raw)
        {
            var comment = BuildNewComment(raw, null, null);
            return PostComment(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="parentCommentId">
        /// The id of the parent comment if this comment is a reply to an existing comment.
        /// </param>
        public TComment PostComment(string raw, int? parentCommentId = null)
        {
            var comment = BuildNewComment(raw, parentCommentId, null);
            return PostComment(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="location">
        /// The location of that comment if it's an inline comment.
        /// </param>
        public TComment PostComment(string raw, Location location)
        {
            var comment = BuildNewComment(raw, null, location);
            return PostComment(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The comment to add.</param>
        public TComment PostComment(TComment comment)
        {
            return SharpBucketV2.Post(comment, BaseUrl);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="token">The cancellation token</param>
        public Task<TComment> PostCommentAsync(string raw, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, null, null);
            return PostCommentAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="parentCommentId">
        /// The id of the parent comment if this comment is a reply to an existing comment.
        /// </param>
        /// <param name="token">The cancellation token</param>
        public Task<TComment> PostCommentAsync(string raw, int? parentCommentId, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, parentCommentId, null);
            return PostCommentAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="location">
        /// The location of that comment if it's an inline comment.
        /// </param>
        /// <param name="token">The cancellation token</param>
        public Task<TComment> PostCommentAsync(string raw, Location location, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, null, location);
            return PostCommentAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The comment to add.</param>
        public Task<TComment> PostCommentAsync(TComment comment, CancellationToken token = default)
        {
            return SharpBucketV2.PostAsync(comment, BaseUrl, token);
        }

        /// <summary>
        /// Get a comment.
        /// </summary>
        /// <param name="commentId">The id of the comment to get.</param>
        public TComment GetComment(int commentId)
        {
            return SharpBucketV2.Get<TComment>(BaseUrl + "/" + commentId);
        }

        /// <summary>
        /// Get a comment.
        /// </summary>
        /// <param name="commentId">The id of the comment to get.</param>
        /// <param name="token">The cancellation token</param>
        public Task<TComment> GetCommentAsync(int commentId, CancellationToken token = default)
        {
            return SharpBucketV2.GetAsync<TComment>(BaseUrl + "/" + commentId, token);
        }

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="comment">The comment object to update.</param>
        public TComment PutComment(TComment comment)
        {
            return SharpBucketV2.Put(BuildPutComment(comment), BaseUrl + "/" + comment.id);
        }

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="comment">The comment object to update.</param>
        /// <param name="token">The cancellation token</param>
        public Task<TComment> PutCommentAsync(TComment comment, CancellationToken token = default)
        {
            return SharpBucketV2.PutAsync(BuildPutComment(comment), BaseUrl + "/" + comment.id, token);
        }

        /// <summary>
        /// Delete the comment.
        /// </summary>
        /// <param name="commentId">The id of the comment to delete.</param>
        public void DeleteComment(int commentId)
        {
            SharpBucketV2.Delete(BaseUrl + "/" + commentId);
        }

        /// <summary>
        /// Delete the comment.
        /// </summary>
        /// <param name="commentId">The id of the comment to delete.</param>
        /// <param name="token">The cancellation token</param>
        public Task DeleteCommentAsync(int commentId, CancellationToken token = default)
        {
            return SharpBucketV2.DeleteAsync(BaseUrl + "/" + commentId, token);
        }

        private TComment BuildNewComment(string raw, int? parentCommentId, Location location)
        {
            var comment = new TComment
            {
                content = new Rendered { raw = raw }
            };
            if (parentCommentId != null)
            {
                comment.parent = new CommentInfo { id = parentCommentId };
            }
            if (location != null)
            {
                comment.inline = location;
            }
            return comment;
        }

        private TComment BuildPutComment(TComment comment)
        {
            return new TComment
            {
                content = new Rendered
                {
                    raw = comment.content.raw
                },
                parent = comment.parent != null
                    ? new CommentInfo
                    {
                        id = comment.parent.id
                    }
                    : null
            };
        }
    }
}