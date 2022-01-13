using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
    /// </summary>
    public class PullRequestsResource : EndPoint
    {
        #region Pull Requests Resource

        public PullRequestsResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "pullrequests")
        {
        }

        /// <summary>
        /// List open pull requests on the repository.
        /// </summary>
        /// <returns></returns>
        public List<PullRequest> ListPullRequests()
            => ListPullRequests(new ListPullRequestsParameters());

        /// <summary>
        /// List pull requests on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<PullRequest> ListPullRequests(ListPullRequestsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<PullRequest>(BaseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate open pull requests on the repository.
        /// </summary>
        public IEnumerable<PullRequest> EnumeratePullRequests()
            => EnumeratePullRequests(new EnumeratePullRequestsParameters());

        /// <summary>
        /// Enumerate pull requests on the repository.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<PullRequest> EnumeratePullRequests(EnumeratePullRequestsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValues<PullRequest>(BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate open pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<PullRequest> EnumeratePullRequestsAsync(CancellationToken token = default)
            => EnumeratePullRequestsAsync(new EnumeratePullRequestsParameters(), token);

        /// <summary>
        /// Enumerate pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<PullRequest> EnumeratePullRequestsAsync(EnumeratePullRequestsParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValuesAsync<PullRequest>(BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Creates a new pull request. The request URL you provide is the destination repository URL. 
        /// For this reason, you must specify an explicit source repository in the request object if you want to pull from a different repository.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public PullRequest PostPullRequest(PullRequest pullRequest)
        {
            return SharpBucketV2.Post(pullRequest, BaseUrl);
        }

        /// <summary>
        /// Creates a new pull request. The request URL you provide is the destination repository URL. 
        /// For this reason, you must specify an explicit source repository in the request object if you want to pull from a different repository.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequest> PostPullRequestAsync(PullRequest pullRequest, CancellationToken token = default)
        {
            return SharpBucketV2.PostAsync(pullRequest, BaseUrl, token);
        }

        /// <summary>
        /// Updates an existing pull request. The pull request's status must be open. 
        /// With the exception of the source and destination parameters, the request body must include all the existing request parameters; 
        /// Omitting a parameter causes the server to drop the existing value. For example, if the pull requests already has 3 reviewers, 
        /// the request body must include these 3 reviewers to prevent Bitbucket from dropping them.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <returns></returns>
        public PullRequest PutPullRequest(PullRequest pullRequest)
        {
            return SharpBucketV2.Put(pullRequest, BaseUrl);
        }

        /// <summary>
        /// Updates an existing pull request. The pull request's status must be open. 
        /// With the exception of the source and destination parameters, the request body must include all the existing request parameters; 
        /// Omitting a parameter causes the server to drop the existing value. For example, if the pull requests already has 3 reviewers, 
        /// the request body must include these 3 reviewers to prevent Bitbucket from dropping them.
        /// </summary>
        /// <param name="pullRequest">The pull request.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<PullRequest> PutPullRequestAsync(PullRequest pullRequest, CancellationToken token = default)
        {
            return SharpBucketV2.PutAsync(pullRequest, BaseUrl, token);
        }

        /// <summary>
        /// Returns activity log for all the pull requests on the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Activity> GetPullRequestsActivities(int max = 0)
        {
            var overrideUrl = BaseUrl + "/activity";
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        /// <summary>
        /// Enumerate activity log for all the pull requests on the repository.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Activity> EnumeratePullRequestsActivities(int? pageLen = null)
        {
            var overrideUrl = BaseUrl + "/activity";
            return SharpBucketV2.EnumeratePaginatedValues<Activity>(overrideUrl, null, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate open pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestsActivitiesAsync(CancellationToken token = default)
            => EnumeratePullRequestsActivitiesAsync(null, token);

        /// <summary>
        /// Enumerate pull requests on the repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The size of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Activity> EnumeratePullRequestsActivitiesAsync(int? pageLen, CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/activity";
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Activity>(overrideUrl, null, pageLen, token);
        }
#endif

        #endregion

        #region Pull request Resource

        /// <summary>
        /// Get the Pull Request Resource.
        /// </summary>
        /// <param name="pullRequestId">The pull request identifier.</param>
        public PullRequestResource PullRequestResource(int pullRequestId)
        {
            return new PullRequestResource(this, pullRequestId);
        }

        #endregion
    }
}
