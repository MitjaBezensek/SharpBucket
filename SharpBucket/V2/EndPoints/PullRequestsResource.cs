using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
    /// </summary>
    public class PullRequestsResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _repository;
        private readonly string _accountName;

        #region Pull Requests Resource

        public PullRequestsResource(string accountName, string repository, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// List all of a repository's open pull requests.
        /// </summary>
        /// <returns></returns>
        public List<PullRequest> ListPullRequests(int max = 100)
        {
            return _repositoriesEndPoint.ListPullRequests(_accountName, _repository, max);
        }

        /// <summary>
        /// Creates a new pull request. The request URL you provide is the destination repository URL. 
        /// For this reason, you must specify an explicit source repository in the request object if you want to pull from a different repository.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public object PostPullRequest(PullRequest pullRequest)
        {
            return _repositoriesEndPoint.PostPullRequest(_accountName, _repository, pullRequest);
        }

        /// <summary>
        /// Updates an existing pull request. The pull request's status must be open. 
        /// With the exception of the source and destination parameters, the request body must include all the existing request parameters; 
        /// Omitting a parameter causes the server to drop the existing value. For example, if the pull requests already has 3 reviewers, 
        /// the request body must include these 3 reviewers to prevent Bitbucket from dropping them.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public object PutPullRequest(PullRequest pullRequest)
        {
            return _repositoriesEndPoint.PutPullRequest(_accountName, _repository, pullRequest);
        }

        /// <summary>
        /// Returns all the pull request activity for a repository. This call returns a historical log of all the pull request activity within a repository.
        /// </summary>
        /// <returns></returns>
        public object GetPullRequestLog()
        {
            return _repositoriesEndPoint.GetPullRequestLog(_accountName, _repository);
        }

        #endregion

        #region Pull request Resource

        /// <summary>
        /// Get the Pull Request Resource.
        /// BitBucket does not have this Resource so this is a "Virtual" resource
        /// which offers easier manipulation of a specific Pull Request.
        /// </summary>
        /// <param name="pullRequestId">The pull request identifier.</param>
        /// <returns></returns>
        public PullRequestResource PullRequestResource(int pullRequestId)
        {
            return new PullRequestResource(_accountName, _repository, pullRequestId, _repositoriesEndPoint);
        }

        internal object GetPullRequest(int pullRequestId)
        {
            return _repositoriesEndPoint.GetPullRequest(_accountName, _repository, pullRequestId);
        }

        internal object ListPullRequestCommits(int pullRequestId)
        {
            return _repositoriesEndPoint.ListPullRequestCommits(_accountName, _repository, pullRequestId);
        }

        internal object ApprovePullRequest(int pullRequestId)
        {
            return _repositoriesEndPoint.ApprovePullRequest(_accountName, _repository, pullRequestId);
        }

        internal object RemovePullRequestApproval(int pullRequestId)
        {
            return _repositoriesEndPoint.RemovePullRequestApproval(_accountName, _repository, pullRequestId);
        }

        internal object GetDiffForPullRequest(int pullRequestId)
        {
            return _repositoriesEndPoint.GetDiffForPullRequest(_accountName, _repository, pullRequestId);
        }

        internal object GetPullRequestActivity(int pullRequestId)
        {
            return _repositoriesEndPoint.GetPullRequestActivity(_accountName, _repository, pullRequestId);
        }

        internal object AcceptAndMergePullRequest(int pullRequestId)
        {
            return _repositoriesEndPoint.AcceptAndMergePullRequest(_accountName, _repository, pullRequestId);
        }

        internal object DeclinePullRequest(int pullRequestId)
        {
            return _repositoriesEndPoint.DeclinePullRequest(_accountName, _repository, pullRequestId);
        }

        internal object ListPullRequestComments(int pullRequestId)
        {
            return _repositoriesEndPoint.ListPullRequestComments(_accountName, _repository, pullRequestId);
        }

        internal object GetPullRequestComment(int pullRequestId, int commentId)
        {
            return _repositoriesEndPoint.GetPullRequestComment(_accountName, _repository, pullRequestId, commentId);
        }

        #endregion
    }
}