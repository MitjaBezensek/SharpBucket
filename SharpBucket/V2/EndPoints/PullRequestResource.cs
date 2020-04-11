using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpull_request_id%7D
    /// and
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests/%7Bpullrequest_id%7D
    /// </summary>
    public class PullRequestResource : EndPoint
    {
        [Obsolete("Prefer PullRequestResource(PullRequestsResource pullRequestsResource, int pullRequestId)")]
        public PullRequestResource(string accountName, string repoSlugOrName, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint)
            : base(
                  repositoriesEndPoint,
                  $"{accountName.GuidOrValue()}/{repoSlugOrName.ToSlug()}/pullrequests/{pullRequestId}")
        {
        }

        public PullRequestResource(PullRequestsResource pullRequestsResource, int pullRequestId)
            : base(pullRequestsResource, pullRequestId.ToString())
        {
        }

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        public PullRequest GetPullRequest()
        {
            return _sharpBucketV2.Get<PullRequest>(_baseUrl);
        }

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequest> GetPullRequestAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync<PullRequest>(_baseUrl, token);
        }

        /// <summary>
        /// List of the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <returns></returns>
        public List<Commit> ListPullRequestCommits()
        {
            var overrideUrl = _baseUrl + "commits";
            return GetPaginatedValues<Commit>(overrideUrl);
        }

        /// <summary>
        /// Enumerate the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Commit> EnumeratePullRequestCommits(int? pageLen = null)
        {
            var overrideUrl = _baseUrl + "commits";
            return _sharpBucketV2.EnumeratePaginatedValues<Commit>(overrideUrl, null, pageLen);
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
            var overrideUrl = _baseUrl + "commits";
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Commit>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <returns></returns>
        public PullRequestInfo ApprovePullRequest()
        {
            var overrideUrl = _baseUrl + "approve";
            return _sharpBucketV2.Post<PullRequestInfo>(null, overrideUrl);
        }

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequestInfo> ApprovePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "approve";
            return _sharpBucketV2.PostAsync<PullRequestInfo>(null, overrideUrl, token);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        public void RemovePullRequestApproval()
        {
            var overrideUrl = _baseUrl + "approve";
            _sharpBucketV2.Delete(overrideUrl);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task RemovePullRequestApprovalAsync(CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "approve";
            return _sharpBucketV2.DeleteAsync(overrideUrl, token);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <returns></returns>
        public string GetDiffForPullRequest()
        {
            var overrideUrl = _baseUrl + "diff";
            return _sharpBucketV2.Get(overrideUrl);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<string> GetDiffForPullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "diff";
            return _sharpBucketV2.GetAsync(overrideUrl, token);
        }

        /// <summary>
        /// Gets a log of the activity for a specific pull request.
        /// </summary>
        [Obsolete("Use ListPullRequestActivities instead which is the exact same method but with a name that respect the global namming rules of the project.")]
        public List<Activity> GetPullRequestActivity()
            => ListPullRequestActivities();

        /// <summary>
        /// List the activities for a specific pull request.
        /// </summary>
        public List<Activity> ListPullRequestActivities()
        {
            var overrideUrl = _baseUrl + "activity";
            return GetPaginatedValues<Activity>(overrideUrl);
        }

        /// <summary>
        /// Enumerate the activities for a specific pull request. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Activity> EnumeratePullRequestActivities(int? pageLen = null)
        {
            var overrideUrl = _baseUrl + "activity";
            return _sharpBucketV2.EnumeratePaginatedValues<Activity>(overrideUrl, null, pageLen);
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
            var overrideUrl = _baseUrl + "activity";
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Activity>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public Merge AcceptAndMergePullRequest()
        {
            var overrideUrl = _baseUrl + "merge";
            return _sharpBucketV2.Post<Merge>(null, overrideUrl);
        }

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<Merge> AcceptAndMergePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "merge";
            return _sharpBucketV2.PostAsync<Merge>(null, overrideUrl, token);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public PullRequest DeclinePullRequest()
        {
            var overrideUrl = _baseUrl + "decline";
            return _sharpBucketV2.Post<PullRequest>(null, overrideUrl);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> DeclinePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "decline";
            return await _sharpBucketV2.PostAsync<PullRequest>(null, overrideUrl, token);
        }

        /// <summary>
        /// List of comments on the specified pull request. 
        /// </summary>
        /// <returns></returns>
        public List<Comment> ListPullRequestComments()
        {
            var overrideUrl = _baseUrl + "comments";
            return GetPaginatedValues<Comment>(overrideUrl);
        }

        /// <summary>
        /// Enumerate the comments on the specified pull request. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Comment> EnumeratePullRequestComments(int? pageLen = null)
        {
            var overrideUrl = _baseUrl + "comments";
            return _sharpBucketV2.EnumeratePaginatedValues<Comment>(overrideUrl, null, pageLen);
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
            var overrideUrl = _baseUrl + "comments";
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Comment>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        public Comment GetPullRequestComment(int commentId)
        {
            var overrideUrl = _baseUrl + $"comments/{commentId}";
            return _sharpBucketV2.Get<Comment>(overrideUrl);
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<Comment> GetPullRequestCommentAsync(int commentId, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"comments/{commentId}";
            return await _sharpBucketV2.GetAsync<Comment>(overrideUrl, token);
        }

        public Comment PostPullRequestComment(Comment comment)
        {
            var overrideUrl = _baseUrl + "comments";
            return _sharpBucketV2.Post(comment, overrideUrl);
        }

        public async Task<Comment> PostPullRequestCommentAsync(Comment comment, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "comments";
            return await _sharpBucketV2.PostAsync(comment, overrideUrl, token);
        }
    }
}
