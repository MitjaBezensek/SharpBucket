using System;
using SharpBucket.Utility;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bnode%7D
    /// and
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commit/%7Bcommit%7D
    /// </summary>
    public class CommitResource : EndPoint
    {
        private readonly Lazy<CommentsResource> _comments;

        internal CommitResource(
            RepositoryResource repositoryResource, string revision)
        : base(repositoryResource, $"commit/{revision}")
        {
            _comments = new Lazy<CommentsResource>(() => new CommentsResource(this));
        }

        /// <summary>
        /// Gets the <see cref="CommentsResource"/> corresponding to this commit.
        /// </summary>
        public CommentsResource Comments => _comments.Value;
    }
}