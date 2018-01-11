using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints
{
    /// <summary>
    /// A "Virtual" End Point that offers easier manipulation of a specific issue.
    /// </summary>
    public class IssueResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly int _issueId;

        public IssueResource(RepositoriesEndPoint repositoriesEndPoint, int issueId)
        {
            _issueId = issueId;
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        public Issue GetIssue()
        {
            return _repositoriesEndPoint.GetIssue(_issueId);
        }

        /// <summary>
        /// Gets the followers for an individual issue from a repository. 
        /// authorization is not required for public repositories with a public issue tracker. 
        /// Private repositories or private issue trackers require the caller to authenticate with an account that has appropriate access.
        /// </summary>
        /// <returns></returns>
        public IssueFollowers ListFollowers()
        {
            return _repositoriesEndPoint.ListIssueFollowers(_issueId);
        }

        /// <summary>
        /// List all the comments of the issue. 
        /// </summary>
        /// <returns></returns>
        public List<Comment> ListComments()
        {
            return _repositoriesEndPoint.ListIssueComments(_issueId);
        }

        /// <summary>
        /// Get a specific comment of the issue.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int? commentId)
        {
            return _repositoriesEndPoint.GetIssueComment(_issueId, commentId);
        }

        /// <summary>
        /// Post a comment to the issue.
        /// </summary>
        /// <param name="comment">The comment you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PostComment(Comment comment)
        {
            return _repositoriesEndPoint.PostIssueComment(_issueId, comment);
        }

        /// <summary>
        /// Update a comment of the current issue.
        /// </summary>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PutIssueComment(Comment comment)
        {
            return _repositoriesEndPoint.PutIssueComment(_issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment of the issue.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment DeleteIssueComment(int? commentId)
        {
            return _repositoriesEndPoint.DeleteIssueComment(_issueId, commentId);
        }
    }
}