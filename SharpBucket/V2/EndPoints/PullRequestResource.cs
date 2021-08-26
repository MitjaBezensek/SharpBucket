using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly Lazy<PullRequestCommentsResource> _commentsResource;

        [Obsolete("Prefer PullRequestResource(PullRequestsResource pullRequestsResource, int pullRequestId)")]
        public PullRequestResource(string accountName, string repoSlugOrName, int pullRequestId, RepositoriesEndPoint repositoriesEndPoint)
            : this(
                  repositoriesEndPoint.RepositoryResource(accountName, repoSlugOrName).PullRequestsResource(),
                  pullRequestId)
        {
        }

        public PullRequestResource(PullRequestsResource pullRequestsResource, int pullRequestId)
            : base(pullRequestsResource, pullRequestId.ToString())
        {
            _commentsResource = new Lazy<PullRequestCommentsResource>(() => new PullRequestCommentsResource(this));
        }

        /// <summary>
        /// Gets the <see cref="CommentResource"/> relative to this pull request.
        /// </summary>
        public PullRequestCommentsResource CommentsResource => _commentsResource.Value;

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        public PullRequest GetPullRequest()
        {
            return SharpBucketV2.Get<PullRequest>(BaseUrl);
        }

        /// <summary>
        /// Gets the <see cref="PullRequest"/>
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequest> GetPullRequestAsync(CancellationToken token = default)
        {
            return SharpBucketV2.GetAsync<PullRequest>(BaseUrl, token);
        }

        /// <summary>
        /// List of the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <returns></returns>
        public List<Commit> ListPullRequestCommits()
        {
            var overrideUrl = BaseUrl + "/commits";
            return GetPaginatedValues<Commit>(overrideUrl);
        }

        /// <summary>
        /// Enumerate the commits associated with a specific pull request, follow the pull request's commits link. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Commit> EnumeratePullRequestCommits(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/commits";
            return SharpBucketV2.EnumeratePaginatedValues<Commit>(overrideUrl, null, pageLen);
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
            var overrideUrl = BaseUrl + "/commits";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Commit>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <returns></returns>
        public PullRequestInfo ApprovePullRequest()
        {
            var overrideUrl = BaseUrl + "/approve";
            return SharpBucketV2.Post<PullRequestInfo>(null, overrideUrl);
        }

        /// <summary>
        /// Give your approval on a pull request. You can only approve a request on behalf of the authenticated account. 
        /// This returns the participant object for the current user.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequestInfo> ApprovePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/approve";
            return SharpBucketV2.PostAsync<PullRequestInfo>(null, overrideUrl, token);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        public void RemovePullRequestApproval()
        {
            var overrideUrl = BaseUrl + "/approve";
            SharpBucketV2.Delete(overrideUrl);
        }

        /// <summary>
        /// Revoke your approval on a pull request. You can remove approvals on behalf of the authenticated account.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task RemovePullRequestApprovalAsync(CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/approve";
            return SharpBucketV2.DeleteAsync(overrideUrl, token);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <returns></returns>
        public string GetDiffForPullRequest()
        {
            var overrideUrl = BaseUrl + "/diff";
            return SharpBucketV2.Get(overrideUrl);
        }

        /// <summary>
        /// Gets the diff or patch for a pull request. This returns a 302 redirect response with the Location header 
        /// set to the URL that will perform a temporary merge and return the diff of it. The result is identical to diff in the UI.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<string> GetDiffForPullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/diff";
            return SharpBucketV2.GetAsync(overrideUrl, token);
        }

        /// <summary>
        /// Gets a log of the activity for a specific pull request.
        /// </summary>
        [Obsolete("Use ListPullRequestActivities instead which is the exact same method but with a name that respect the global naming rules of the project.")]
        public List<Activity> GetPullRequestActivity()
            => ListPullRequestActivities();

        /// <summary>
        /// List the activities for a specific pull request.
        /// </summary>
        public List<Activity> ListPullRequestActivities()
        {
            var overrideUrl = BaseUrl + "/activity";
            return GetPaginatedValues<Activity>(overrideUrl);
        }

        /// <summary>
        /// Enumerate the activities for a specific pull request. This returns a paginated response.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Activity> EnumeratePullRequestActivities(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/activity";
            return SharpBucketV2.EnumeratePaginatedValues<Activity>(overrideUrl, null, pageLen);
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
            var overrideUrl = BaseUrl + "/activity";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Activity>(overrideUrl, null, pageLen, token);
        }
#endif

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public Merge AcceptAndMergePullRequest()
        {
            var overrideUrl = BaseUrl + "/merge";
            return SharpBucketV2.Post<Merge>(null, overrideUrl);
        }

        /// <summary>
        /// Accept a pull request and merges into the destination branch. This requires write access on the destination repository.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<Merge> AcceptAndMergePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/merge";
            return SharpBucketV2.PostAsync<Merge>(null, overrideUrl, token);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <returns></returns>
        public PullRequest DeclinePullRequest()
        {
            var overrideUrl = BaseUrl + "/decline";
            return SharpBucketV2.Post<PullRequest>(null, overrideUrl);
        }

        /// <summary>
        /// Rejects a pull request. This requires write access on the destination repository.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<PullRequest> DeclinePullRequestAsync(CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/decline";
            return await SharpBucketV2.PostAsync<PullRequest>(null, overrideUrl, token);
        }

        /// <summary>
        /// List of comments on the specified pull request. 
        /// </summary>
        [Obsolete("Prefer CommentsResource.ListComments()")]
        public List<Comment> ListPullRequestComments()
        {
            return CommentsResource.EnumerateComments().Cast<Comment>().ToList();
        }

        /// <summary>
        /// Gets an individual comment on an request. Private repositories require authorization with an account that has appropriate access.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        [Obsolete("Prefer CommentsResource.GetComment(commentId)")]
        public Comment GetPullRequestComment(int commentId)
        {
            return CommentsResource.GetComment(commentId);
        }

        [Obsolete("Prefer CommentsResource.PostComment(comment)")]
        public Comment PostPullRequestComment(Comment comment)
        {
            var overrideUrl = BaseUrl + "/comments";
            return SharpBucketV2.Post(comment, overrideUrl);
        }
    }
}
