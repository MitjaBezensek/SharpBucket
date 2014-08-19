using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
    /// </summary>
    public class PullRequestsResourceV2{
        private readonly RepositoriesEndPointV2 _repositoriesEndPointV2;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestsResourceV2(string accountName, string repository, RepositoriesEndPointV2 repositoriesEndPointV2){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
        }

        /// <summary>
        /// Get the Pull Request Resource.
        /// BitBucket does not have this Resource so this is a "Virtual" resource
        /// which offers easier manipulation of the specific Pull Request.
        /// </summary>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        public PullRequestResourceV2 PullRequestResource(int pullRequestId){
            return new PullRequestResourceV2(_accountName, _repository, pullRequestId, _repositoriesEndPointV2);
        }

        /// <summary>
        /// List all of the open pull requests for the current repository.
        /// </summary>
        /// <returns></returns>
        public PullRequestsInfo ListPullRequests(){
            return _repositoriesEndPointV2.ListPullRequests(_accountName, _repository);
        }

        /// <summary>
        /// Add a pull request to the current repository.
        /// </summary>
        /// <param name="pullRequest">The pull request that you wish to add.</param>
        /// <returns></returns>
        public object PostPullRequest(PullRequest pullRequest){
            return _repositoriesEndPointV2.PostPullRequest(_accountName, _repository, pullRequest);
        }

        /// <summary>
        /// Update a pull request of the current repository.
        /// </summary>
        /// <param name="pullRequest">The pull request that you wish to update.</param>
        /// <returns></returns>
        public object PutPullRequest(PullRequest pullRequest){
            return _repositoriesEndPointV2.PutPullRequest(_accountName, _repository, pullRequest);
        }

        /// <summary>
        /// Get a specific pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request that you wish to get.</param>
        /// <returns></returns>
        public object GetPullRequest(int pullRequestId){
            return _repositoriesEndPointV2.GetPullRequest(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// List all of the commits of the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose commits you wish to get.</param>
        /// <returns></returns>
        public object ListPullRequestCommits(int pullRequestId){
            return _repositoriesEndPointV2.ListPullRequestCommits(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Approve the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request that you wish to approve.</param>
        /// <returns></returns>
        public object ApprovePullRequest(int pullRequestId){
            return _repositoriesEndPointV2.ApprovePullRequest(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Remove the approval of the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose approval you wish to remove.</param>
        /// <returns></returns>
        public object RemovePullRequestApproval(int pullRequestId){
            return _repositoriesEndPointV2.RemovePullRequestApproval(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Get the diff for the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose diff you wish to get.</param>
        /// <returns></returns>
        public object GetDiffForPullRequest(int pullRequestId){
            return _repositoriesEndPointV2.GetDiffForPullRequest(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Get the pull request log for the current repository.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequestLog(){
            return _repositoriesEndPointV2.GetPullRequestLog(_accountName, _repository);
        }

        /// <summary>
        /// Get the pull request activity for the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose activity you wish to get.</param>
        /// <returns></returns>
        public object GetPullRequestActivity(int pullRequestId){
            return _repositoriesEndPointV2.GetPullRequestActivity(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Accept and merge the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request that you wish to accept and merge.</param>
        /// <returns></returns>
        public object AcceptAndMergePullRequest(int pullRequestId){
            return _repositoriesEndPointV2.AcceptAndMergePullRequest(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Decline the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request that you wish to decline.</param>
        /// <returns></returns>
        public object DeclinePullRequest(int pullRequestId){
            return _repositoriesEndPointV2.DeclinePullRequest(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// List all the comments of the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose comments you wish to get.</param>
        /// <returns></returns>
        public object ListPullRequestComments(int pullRequestId){
            return _repositoriesEndPointV2.ListPullRequestComments(_accountName, _repository, pullRequestId);
        }

        /// <summary>
        /// Get a specific comment of the selected pull request of the current repository.
        /// </summary>
        /// <param name="pullRequestId">The Id of the pull request whose comment you wish to get.</param>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public object GetPullRequestComment(int pullRequestId, int commentId){
            return _repositoriesEndPointV2.GetPullRequestComment(_accountName, _repository, pullRequestId, commentId);
        }
    }
}