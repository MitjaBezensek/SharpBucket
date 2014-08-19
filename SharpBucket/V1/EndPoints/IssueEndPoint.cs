using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class IssueEndPoint{
        private readonly RepositoriesEndPointV1 _repositoriesEndPointV1;
        private readonly int _issueId;

        public IssueEndPoint(RepositoriesEndPointV1 repositoriesEndPointV1, int issueId){
            _issueId = issueId;
            _repositoriesEndPointV1 = repositoriesEndPointV1;
        }

        /// <summary>
        /// List all the comments for the current issue.
        /// </summary>
        /// <returns></returns>
        public List<Comment> ListComments(){
            return _repositoriesEndPointV1.ListIssueComments(_issueId);
        }

        /// <summary>
        /// Post a new comment for the current issue.
        /// </summary>
        /// <param name="comment">The comment you wish to post.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PostComment(Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(_issueId, comment);
        }

        /// <summary>
        /// Get a specific comment for the current issue.
        /// </summary>
        /// <param name="commentId">The Id of the comment you wish to get.</param>
        /// <returns></returns>
        public Comment GetIssueComment(int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(_issueId, commentId);
        }

        /// <summary>
        /// Update a comment of the current issue.
        /// </summary>
        /// <param name="comment">The comment that you wish to update.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment PutIssueComment(Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(_issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment from the current issue.
        /// </summary>
        /// <param name="comment">The comment that you wish to delete.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment DeleteIssueComment(Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(_issueId, comment);
        }

        /// <summary>
        /// Delete a specific comment from the current issue.
        /// </summary>
        /// <param name="commentId">The Id of the comment you wish to delete.</param>
        /// <returns>Response from the BitBucket API.</returns>
        public Comment DeleteIssueComment(int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(_issueId, commentId);
        }
    }
}