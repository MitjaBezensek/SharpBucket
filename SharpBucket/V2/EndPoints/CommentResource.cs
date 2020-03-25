using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bnode%7D/comments/%7Bcomment_id%7D
    /// and
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D/comments/%7Bcomment_id%7D#put
    /// </summary>
    public class CommentResource : EndPoint
    {
        internal CommentResource(CommentsResource commentsResource, int commentId)
            : base(commentsResource, commentId.ToString())
        {
        }

        /// <summary>
        /// Get the comment.
        /// </summary>
        public Comment Get()
        {
            return _sharpBucketV2.Get<Comment>(_baseUrl);
        }

        /// <summary>
        /// Get the comment.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task<Comment> GetAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync<Comment>(_baseUrl, token);
        }

        /// <summary>
        /// Update the comment.
        /// </summary>
        public Comment Put(Comment comment)
        {
            return _sharpBucketV2.Put(BuildPutComment(comment), _baseUrl);
        }

        /// <summary>
        /// Update the comment.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task<Comment> PutAsync(Comment comment, CancellationToken token = default)
        {
            return _sharpBucketV2.PutAsync(BuildPutComment(comment), _baseUrl, token);
        }

        /// <summary>
        /// Delete the comment.
        /// </summary>
        public void Delete()
        {
            _sharpBucketV2.Delete(_baseUrl);
        }

        /// <summary>
        /// Delete the comment.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public Task DeleteAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.DeleteAsync(_baseUrl, token);
        }

        private Comment BuildPutComment(Comment comment)
        {
            return new Comment
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
