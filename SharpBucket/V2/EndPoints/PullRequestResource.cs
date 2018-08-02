using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// A "Virtual" resource that offers easier manipulation of the pull request.
    /// </summary>
    public class PullRequestResource
    {
        private readonly int _pullRequestId;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestResource(string accountName, string repository, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPoint = repositoriesEndPoint;
            _pullRequestId = pullRequestId;
        }

        /// <summary>
        /// List all the pull requests info for the repository.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequest()
        {
            return _repositoriesEndPoint.GetPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List of the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestCommits()
        {
            return _repositoriesEndPoint.ListPullRequestCommits(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <returns></returns>
        public object ApprovePullRequest()
        {
            return _repositoriesEndPoint.ApprovePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        /// <returns></returns>
        public object RemovePullRequestApproval()
        {
            return _repositoriesEndPoint.RemovePullRequestApproval(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <returns></returns>
        public object GetDiffForPullRequest()
        {
            return _repositoriesEndPoint.GetDiffForPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Gets a log of the activity for a specific pull request.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequestActivity()
        {
            return _repositoriesEndPoint.GetPullRequestActivity(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public object AcceptAndMergePullRequest()
        {
            return _repositoriesEndPoint.AcceptAndMergePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public object DeclinePullRequest()
        {
            return _repositoriesEndPoint.DeclinePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List of comments on the specified pull request. 
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestComments()
        {
            return _repositoriesEndPoint.ListPullRequestComments(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>      /// <returns></returns>
        public object GetPullRequestComment(int commentId)
        {
            return _repositoriesEndPoint.GetPullRequestComment(_accountName, _repository, _pullRequestId, commentId);
        }

        public Comment PostPullRequstComment(Comment comment)
        {
            return _repositoriesEndPoint.PostPullRequestComment(_accountName, _repository, _pullRequestId, comment);
        }
    }
}