namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// A "Virtual" resource that offers easier manipulation of the pull request.
    /// </summary>
    public class PullRequestResourceV2{
        private readonly int _pullRequestId;
        private readonly RepositoriesEndPointV2 _repositoriesEndPointV2;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestResourceV2(string accountName, string repository, int pullRequestId, RepositoriesEndPointV2 repositoriesEndPointV2){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
            _pullRequestId = pullRequestId;
        }

        /// <summary>
        /// Get the information about the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequest(){
            return _repositoriesEndPointV2.GetPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List all the commits of the pull request.
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestCommits(){
            return _repositoriesEndPointV2.ListPullRequestCommits(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Approve the pull request.
        /// </summary>
        /// <returns></returns>
        public object ApprovePullRequest(){
            return _repositoriesEndPointV2.ApprovePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Remove the pull request approval.
        /// </summary>
        /// <returns></returns>
        public object RemovePullRequestApproval(){
            return _repositoriesEndPointV2.RemovePullRequestApproval(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get the diff for the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetDiffForPullRequest(){
            return _repositoriesEndPointV2.GetDiffForPullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get the activity for the pull request.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequestActivity(){
            return _repositoriesEndPointV2.GetPullRequestActivity(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Accept and merge the pull request.
        /// </summary>
        /// <returns></returns>
        public object AcceptAndMergePullRequest(){
            return _repositoriesEndPointV2.AcceptAndMergePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Decline the pull request.
        /// </summary>
        /// <returns></returns>
        public object DeclinePullRequest(){
            return _repositoriesEndPointV2.DeclinePullRequest(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// List the pull requests comments.
        /// </summary>
        /// <returns></returns>
        public object ListPullRequestComments(){
            return _repositoriesEndPointV2.ListPullRequestComments(_accountName, _repository, _pullRequestId);
        }

        /// <summary>
        /// Get a specific comment of the pull request.
        /// </summary>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public object GetPullRequestComment(int commentId){
            return _repositoriesEndPointV2.GetPullRequestComment(_accountName, _repository, _pullRequestId, commentId);
        }
    }
}