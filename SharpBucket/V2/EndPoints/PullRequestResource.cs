namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// A "Virtual" resource that offers easier manipulation of the pull request.
    /// </summary>
    public class PullRequestResource{
        private readonly int _pullRequestId;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestResource(string accountName, string repository, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPoint = repositoriesEndPoint;
            _pullRequestId = pullRequestId;
        }

        /// <summary>
        /// Get the information about the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequest(){
            return _repositoriesEndPoint.GetPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List all the commits of the pull request.
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestCommits(){
            return _repositoriesEndPoint.ListPullRequestCommits(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Approve the pull request.
        /// </summary>
        /// <returns></returns>
        public object ApprovePullRequest(){
            return _repositoriesEndPoint.ApprovePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Remove the pull request approval.
        /// </summary>
        /// <returns></returns>
        public object RemovePullRequestApproval(){
            return _repositoriesEndPoint.RemovePullRequestApproval(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get the diff for the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetDiffForPullRequest(){
            return _repositoriesEndPoint.GetDiffForPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get the activity for the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequestActivity(){
            return _repositoriesEndPoint.GetPullRequestActivity(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Accept and merge the pull request.
        /// </summary>
        /// <returns></returns>
        public object AcceptAndMergePullRequest(){
            return _repositoriesEndPoint.AcceptAndMergePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Decline the pull request.
        /// </summary>
        /// <returns></returns>
        public object DeclinePullRequest(){
            return _repositoriesEndPoint.DeclinePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List the pull requests comments.
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestComments(){
            return _repositoriesEndPoint.ListPullRequestComments(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get a specific comment of the pull request.
        /// </summary>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public object GetPullRequestComment(int commentId){
            return _repositoriesEndPoint.GetPullRequestComment(_accountName, _repository, _pullRequestId, commentId);
        }
    }
}