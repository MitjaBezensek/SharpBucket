using System;
using System.Collections.Generic;
using System.Threading;
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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> GetPullRequestAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetPullRequestAsync(_accountName, _slug, _pullRequestId, token);
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
        /// Enumerate the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Commit> EnumeratePullRequestCommits(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumeratePullRequestCommits(_accountName, _slug, _pullRequestId, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the commits associated with a specific pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Commit> EnumeratePullRequestCommitsAsync(CancellationToken token = default)
            => EnumeratePullRequestCommitsAsync(null, token);

        /// <summary>
        /// Enumerate the commits associated with a specific pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Commit> EnumeratePullRequestCommitsAsync(int? pageLen, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumeratePullRequestCommitsAsync(_accountName, _slug, _pullRequestId, pageLen, token);
        }
#endif

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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequestInfo> ApprovePullRequestAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.ApprovePullRequestAsync(_accountName, _slug, _pullRequestId, token);
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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task RemovePullRequestApprovalAsync(CancellationToken token = default)
        {
            await _repositoriesEndPoint.RemovePullRequestApprovalAsync(_accountName, _slug, _pullRequestId, token);
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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<string> GetDiffForPullRequestAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetDiffForPullRequestAsync(_accountName, _slug, _pullRequestId, token);
        }

        /// <summary>
        /// Gets a log of the activity for a specific pull request.
        /// </summary>
        [Obsolete("Use ListPullRequestActivities instead which is the exact same method but with a name that respect the global namming rules of the project.")]
        public List<Activity> GetPullRequestActivity() => ListPullRequestActivities();

        /// <summary>
        /// List the activities for a specific pull request.
        /// </summary>
        public List<Activity> ListPullRequestActivities()
        {
            return _repositoriesEndPoint.ListPullRequestActivities(_accountName, _slug, _pullRequestId);
        }

        /// <summary>
        /// Enumerate the activities for a specific pull request. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Activity> EnumeratePullRequestActivities(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumeratePullRequestActivities(_accountName, _slug, _pullRequestId, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the activities for a specific pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestActivitiesAsync(CancellationToken token = default)
            => EnumeratePullRequestActivitiesAsync(null, token);

        /// <summary>
        /// Enumerate the activities for a specific pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestActivitiesAsync(int? pageLen, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumeratePullRequestActivitiesAsync(_accountName, _slug, _pullRequestId, pageLen, token);
        }
#endif

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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<Merge> AcceptAndMergePullRequestAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.AcceptAndMergePullRequestAsync(_accountName, _slug, _pullRequestId, token);
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
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> DeclinePullRequestAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.DeclinePullRequestAsync(_accountName, _slug, _pullRequestId, token);
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
        /// Enumerate the comments on the specified pull request. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Comment> EnumeratePullRequestComments(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumeratePullRequestComments(_accountName, _slug, _pullRequestId, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the comments on the specified pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Comment> EnumeratePullRequestCommentsAsync(CancellationToken token = default)
            => EnumeratePullRequestCommentsAsync(null, token);

        /// <summary>
        /// Enumerate the comments on the specified pull request asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Comment> EnumeratePullRequestCommentsAsync(int? pageLen, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumeratePullRequestCommentsAsync(_accountName, _slug, _pullRequestId, pageLen, token);
        }
#endif

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        public Comment GetPullRequestComment(int commentId)
        {
            return _repositoriesEndPoint.GetPullRequestComment(_accountName, _slug, _pullRequestId, commentId);
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<Comment> GetPullRequestCommentAsync(int commentId, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetPullRequestCommentAsync(_accountName, _slug, _pullRequestId, commentId, token);
        }

        public Comment PostPullRequestComment(Comment comment)
        {
            return _repositoriesEndPoint.PostPullRequestComment(_accountName, _slug, _pullRequestId, comment);
        }

        public async Task<Comment> PostPullRequestCommentAsync(Comment comment, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PostPullRequestCommentAsync(_accountName, _slug, _pullRequestId, comment, token);
        }
    }
}
