using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bnode%7D/comments
    /// and
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments
    /// </summary>
    public class CommentsResource : EndPoint
    {
        internal CommentsResource(CommitResource commitResource)
            : this(commitResource as EndPoint)
        {
        }

        internal CommentsResource(PullRequestResource pullRequestResource)
            : this(pullRequestResource as EndPoint)
        {
        }

        private CommentsResource(EndPoint parentResource)
            : base(parentResource, "comments/")
        {
        }

        /// <summary>
        /// List comments associated with this resource.
        /// </summary>
        public List<Comment> List()
            => List(new ListParameters());

        /// <summary>
        /// List comments associated with this resource.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Comment> List(ListParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.GetPaginatedValues<Comment>(
                _baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing requests page by page while enumerating.
        /// </summary>
        public IEnumerable<Comment> Enumerate()
            => Enumerate(new EnumerateParameters());

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Comment> Enumerate(EnumerateParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValues<Comment>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing async requests page by page while enumerating.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<Comment> EnumerateAsync(CancellationToken token = default)
            => EnumerateAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate comments associated with this resource,
        /// doing async requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<Comment> EnumerateAsync(
            EnumerateParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Comment>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        public Comment Post(string raw)
        {
            var comment = BuildNewComment(raw, null, null);
            return Post(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="parentCommentId">
        /// The id of the parent comment if this comment is a reply to an existing comment.
        /// </param>
        public Comment Post(string raw, int? parentCommentId = null)
        {
            var comment = BuildNewComment(raw, parentCommentId, null);
            return Post(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="location">
        /// The location of that comment if it's an inline comment.
        /// </param>
        public Comment Post(string raw, Location location)
        {
            var comment = BuildNewComment(raw, null, location);
            return Post(comment);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The comment to add.</param>
        public Comment Post(Comment comment)
        {
            return _sharpBucketV2.Post(comment, _baseUrl);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="token">The cancellation token</param>
        public Task<Comment> PostAsync(string raw, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, null, null);
            return PostAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="parentCommentId">
        /// The id of the parent comment if this comment is a reply to an existing comment.
        /// </param>
        /// <param name="token">The cancellation token</param>
        public Task<Comment> PostAsync(string raw, int? parentCommentId, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, parentCommentId, null);
            return PostAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The raw texte of the comment.</param>
        /// <param name="location">
        /// The location of that comment if it's an inline comment.
        /// </param>
        /// <param name="token">The cancellation token</param>
        public Task<Comment> PostAsync(string raw, Location location, CancellationToken token = default)
        {
            var comment = BuildNewComment(raw, null, location);
            return PostAsync(comment, token);
        }

        /// <summary>
        /// Add the specified comment.
        /// </summary>
        /// <param name="raw">The comment to add.</param>
        public Task<Comment> PostAsync(Comment comment, CancellationToken token = default)
        {
            return _sharpBucketV2.PostAsync(comment, _baseUrl, token);
        }

        private Comment BuildNewComment(string raw, int? parentCommentId, Location location)
        {
            var comment = new Comment
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

        /// <summary>
        /// Gets the <see cref="CommentResource"/> for the specified <paramref name="commentId"/>.
        /// </summary>
        /// <param name="commentId">The id of a comment in this resource.</param>
        public CommentResource Comment(int commentId)
        {
            return new CommentResource(this, commentId);
        }
    }
}
