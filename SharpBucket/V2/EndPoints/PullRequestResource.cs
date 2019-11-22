using System.Collections.Generic;
using System.Threading.Tasks;
using SharpBucket.Utility;
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
        private readonly string _slug;
        private readonly string _accountName;

        public PullRequestResource(string accountName, string repoSlugOrName, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
            _pullRequestId = pullRequestId;
        }

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        public PullRequest GetPullRequest()
        {
            return _repositoriesEndPoint.GetPullRequest(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        public async Task<PullRequest> GetPullRequestAsync()
        {
            return await _repositoriesEndPoint.GetPullRequestAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// List of the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <returns></returns>
        public List<Commit> ListPullRequestCommits()
        {
            return _repositoriesEndPoint.ListPullRequestCommits(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <returns></returns>
        public PullRequestInfo ApprovePullRequest()
        {
            return _repositoriesEndPoint.ApprovePullRequest(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <returns></returns>
        public async Task<PullRequestInfo> ApprovePullRequestAsync()
        {
            return await _repositoriesEndPoint.ApprovePullRequestAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        public void RemovePullRequestApproval()
        {
            _repositoriesEndPoint.RemovePullRequestApproval(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        public async Task RemovePullRequestApprovalAsync()
        {
            await _repositoriesEndPoint.RemovePullRequestApprovalAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <returns></returns>
        public string GetDiffForPullRequest()
        {
            return _repositoriesEndPoint.GetDiffForPullRequest(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetDiffForPullRequestAsync()
        {
            return await _repositoriesEndPoint.GetDiffForPullRequestAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Gets a log of the activity for a specific pull request.
        /// </summary>
        /// <returns></returns>
        public List<Activity> GetPullRequestActivity()
        {
            return _repositoriesEndPoint.GetPullRequestActivity(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public Merge AcceptAndMergePullRequest()
        {
            return _repositoriesEndPoint.AcceptAndMergePullRequest(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public async Task<Merge> AcceptAndMergePullRequestAsync()
        {
            return await _repositoriesEndPoint.AcceptAndMergePullRequestAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public PullRequest DeclinePullRequest()
        {
            return _repositoriesEndPoint.DeclinePullRequest(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public async Task<PullRequest> DeclinePullRequestAsync()
        {
            return await _repositoriesEndPoint.DeclinePullRequestAsync(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// List of comments on the specified pull request. 
        /// </summary>
        /// <returns></returns>
        public List<Comment> ListPullRequestComments()
        {
            return _repositoriesEndPoint.ListPullRequestComments(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>      /// <returns></returns>
        public Comment GetPullRequestComment(int commentId)
        {
            return _repositoriesEndPoint.GetPullRequestComment(_accountName, _slug, _pullRequestId, commentId);
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>      /// <returns></returns>
        public async Task<Comment> GetPullRequestCommentAsync(int commentId)
        {
            return await _repositoriesEndPoint.GetPullRequestCommentAsync(_accountName, _slug, _pullRequestId, commentId);
        }

        public Comment PostPullRequestComment(Comment comment)
        {
            return _repositoriesEndPoint.PostPullRequestComment(_accountName, _slug, _pullRequestId, comment);
        }

        public async Task<Comment> PostPullRequestCommentAsync(Comment comment)
        {
            return await _repositoriesEndPoint.PostPullRequestCommentAsync(_accountName, _slug, _pullRequestId, comment);
        }
    }
}
