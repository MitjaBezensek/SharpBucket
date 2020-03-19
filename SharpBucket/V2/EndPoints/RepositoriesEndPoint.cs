﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;
using Comment = SharpBucket.V2.Pocos.Comment;
using Repository = SharpBucket.V2.Pocos.Repository;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The repositories endpoint has a number of resources you can use to manage repository resources. 
    /// For all repository resources, you supply a  repo_slug that identifies the specific repository.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories
    /// </summary>
    public class RepositoriesEndPoint : EndPoint
    {
        #region Repositories End Point

        public RepositoriesEndPoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "repositories/")
        {
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(int max = 0)
        {
            return GetPaginatedValues<Repository>(_baseUrl, max);
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(ListPublicRepositoriesParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return GetPaginatedValues<Repository>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Repository> EnumeratePublicRepositories(EnumeratePublicRepositoriesParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValues<Repository>(_baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all public repositories on BitBucket, doing requests page by page.
        /// </summary>m>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Repository> EnumeratePublicRepositoriesAsync(EnumeratePublicRepositoriesParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(_baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        #endregion

        #region Repositories Account Resource

        /// <summary>
        /// Gets the <see cref="RepositoriesAccountResource"/> that will allow to list the repositories of a specified
        /// account.
        /// </summary>
        /// <param name="accountName"The account whose repositories you wish to get.></param>
        public RepositoriesAccountResource RepositoriesResource(string accountName)
        {
            return new RepositoriesAccountResource(_sharpBucketV2, accountName, this);
        }

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <returns></returns>
        [Obsolete("Prefer go through the RepositoriesAccountResource(accountName) method.")]
        public List<Repository> ListRepositories(string accountName)
            => RepositoriesResource(accountName).ListRepositories();

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        [Obsolete("Prefer go through the RepositoriesAccountResource(accountName) method, and use a method using a ListRepositoriesParameters.")]
        public List<Repository> ListRepositories(string accountName, ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var overrideUrl = $"{_baseUrl}{accountName.GuidOrValue()}/";
            return GetPaginatedValues<Repository>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        #endregion

        #region Repository resource

        /// <summary>
        /// Use this resource to get information associated with an individual repository. You can use these calls with public or private repositories. 
        /// Private repositories require the caller to authenticate with an account that has the appropriate authorization.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        /// <returns></returns>
        public RepositoryResource RepositoryResource(string accountName, string repoSlugOrName)
        {
            return new RepositoryResource(accountName, repoSlugOrName, this);
        }

        internal Repository GetRepository(string accountName, string slug)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            return _sharpBucketV2.Get<Repository>(overrideUrl);
        }

        internal async Task<Repository> GetRepositoryAsync(string accountName, string slug, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            return await _sharpBucketV2.GetAsync<Repository>(overrideUrl, token: token);
        }

        internal Repository PutRepository(Repository repo, string accountName, string slug)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        internal async Task<Repository> PutRepositoryAsync(Repository repo, string accountName, string slug, CancellationToken token)
        {
            var overrideURL = GetRepositoryUrl(accountName, slug, null);
            return await _sharpBucketV2.PutAsync(repo, overrideURL, token: token);
        }

        internal Repository PostRepository(Repository repo, string accountName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name.ToSlug(), null);
            return _sharpBucketV2.Post(repo, overrideUrl);
        }

        internal async Task<Repository> PostRepositoryAsync(Repository repo, string accountName, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name.ToSlug(), null);
            return await _sharpBucketV2.PostAsync(repo, overrideUrl, token);
        }

        internal void DeleteRepository(string accountName, string slug)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            _sharpBucketV2.Delete(overrideUrl);
        }

        internal async Task DeleteRepositoryAsync(string accountName, string slug, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            await _sharpBucketV2.DeleteAsync(overrideUrl, token);
        }

        private string GetRepositoryUrl(string accountName, string slug, string append)
        {
            return $"{_baseUrl}{accountName}/{slug}/{append}";
        }

        internal List<UserShort> ListWatchers(string accountName, string slug, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "watchers");
            return GetPaginatedValues<UserShort>(overrideUrl, max);
        }

        internal IEnumerable<UserShort> EnumerateWatchers(string accountName, string slug, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "watchers");
            return _sharpBucketV2.EnumeratePaginatedValues<UserShort>(overrideUrl, pageLen: pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<UserShort> EnumerateWatchersAsync(string accountName, string slug, int? pageLen, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "watchers");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<UserShort>(overrideUrl, new Dictionary<string, object>(), pageLen, token);
        }
#endif

        internal List<Repository> ListForks(string accountName, string slug, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "forks");
            return GetPaginatedValues<Repository>(overrideUrl, max);
        }

        internal IEnumerable<Repository> EnumerateForks(string accountName, string slug, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "forks");
            return _sharpBucketV2.EnumeratePaginatedValues<Repository>(overrideUrl, pageLen: pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Repository> EnumerateForksAsync(string accountName, string slug, int? pageLen, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "forks");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(overrideUrl, new Dictionary<string, object>(), pageLen, token);
        }
#endif

        #endregion

        #region Pull Requests Resource

        /// <summary>
        /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        /// <returns></returns>
        public PullRequestsResource PullRequestsResource(string accountName, string repoSlugOrName)
        {
            return new PullRequestsResource(accountName, repoSlugOrName, this);
        }

        [Obsolete("Prefer the ListPullRequests(string, string, ListPullRequestsParameters) overload.")]
        internal List<PullRequest> ListPullRequests(string accountName, string slug, ListParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return GetPaginatedValues<PullRequest>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        internal List<PullRequest> ListPullRequests(string accountName, string slug, ListPullRequestsParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return GetPaginatedValues<PullRequest>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        internal IEnumerable<PullRequest> EnumeratePullRequests(string accountName, string slug, EnumeratePullRequestsParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.EnumeratePaginatedValues<PullRequest>(overrideUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        internal IAsyncEnumerable<PullRequest> EnumeratePullRequestsAsync(string accountName, string slug, EnumeratePullRequestsParameters parameters, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<PullRequest>(overrideUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        internal PullRequest PostPullRequest(string accountName, string slug, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        internal async Task<PullRequest> PostPullRequestAsync(string accountName, string slug, PullRequest pullRequest, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return await _sharpBucketV2.PostAsync(pullRequest, overrideUrl, token);
        }

        internal PullRequest PutPullRequest(string accountName, string slug, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }

        internal async Task<PullRequest> PutPullRequestAsync(string accountName, string slug, PullRequest pullRequest, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return await _sharpBucketV2.PutAsync(pullRequest, overrideUrl, token);
        }

        internal List<Activity> GetPullRequestsActivities(string accountName, string slug, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        internal IEnumerable<Activity> EnumeratePullRequestsActivities(string accountName, string slug, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/activity/");
            return _sharpBucketV2.EnumeratePaginatedValues<Activity>(overrideUrl, null, pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Activity> EnumeratePullRequestsActivitiesAsync(string accountName, string slug, int? pageLen, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/activity/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Activity>(overrideUrl, null, pageLen, token);
        }
#endif

        #endregion

        #region Pull Request Resource

        internal PullRequest GetPullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/");
            return _sharpBucketV2.Get<PullRequest>(overrideUrl);
        }

        internal async Task<PullRequest> GetPullRequestAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/");
            return await _sharpBucketV2.GetAsync<PullRequest>(overrideUrl, token);
        }

        internal List<Commit> ListPullRequestCommits(string accountName, string slug, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/commits/");
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        internal IEnumerable<Commit> EnumeratePullRequestCommits(string accountName, string slug, int pullRequestId, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/commits/");
            return _sharpBucketV2.EnumeratePaginatedValues<Commit>(overrideUrl, null, pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Commit> EnumeratePullRequestCommitsAsync(string accountName, string slug, int pullRequestId, int? pageLen, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/commits/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Commit>(overrideUrl, null, pageLen, token);
        }
#endif

        internal PullRequestInfo ApprovePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            return _sharpBucketV2.Post<PullRequestInfo>(null, overrideUrl);
        }

        internal async Task<PullRequestInfo> ApprovePullRequestAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            return await _sharpBucketV2.PostAsync<PullRequestInfo>(null, overrideUrl, token);
        }

        internal void RemovePullRequestApproval(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            _sharpBucketV2.Delete(overrideUrl);
        }

        internal async Task RemovePullRequestApprovalAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            await _sharpBucketV2.DeleteAsync(overrideUrl, token);
        }

        internal string GetDiffForPullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/diff/");
            return _sharpBucketV2.Get(overrideUrl);
        }

        internal async Task<string> GetDiffForPullRequestAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/diff/");
            return await _sharpBucketV2.GetAsync(overrideUrl, token);
        }

        internal List<Activity> ListPullRequestActivities(string accountName, string slug, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        internal IEnumerable<Activity> EnumeratePullRequestActivities(string accountName, string slug, int pullRequestId, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/activity/");
            return _sharpBucketV2.EnumeratePaginatedValues<Activity>(overrideUrl, null, pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Activity> EnumeratePullRequestActivitiesAsync(string accountName, string slug, int pullRequestId, int? pageLen, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/activity/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Activity>(overrideUrl, null, pageLen, token);
        }
#endif

        internal Merge AcceptAndMergePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/merge/");
            return _sharpBucketV2.Post<Merge>(null, overrideUrl);
        }

        internal async Task<Merge> AcceptAndMergePullRequestAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/merge/");
            return await _sharpBucketV2.PostAsync<Merge>(null, overrideUrl, token);
        }

        internal PullRequest DeclinePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/decline/");
            return _sharpBucketV2.Post<PullRequest>(null, overrideUrl);
        }

        internal async Task<PullRequest> DeclinePullRequestAsync(string accountName, string slug, int pullRequestId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/decline/");
            return await _sharpBucketV2.PostAsync<PullRequest>(null, overrideUrl, token);
        }

        #endregion

        #region Branch Restrictions resource

        internal List<BranchRestriction> ListBranchRestrictions(string accountName, string slug, BranchRestrictionsParameters parameters, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return GetPaginatedValues<BranchRestriction>(overrideUrl, max, parameters.ToDictionary());
        }

        internal IEnumerable<BranchRestriction> EnumerateBranchRestrictions(string accountName, string slug, BranchRestrictionsParameters parameters, int? pageLen)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return _sharpBucketV2.EnumeratePaginatedValues<BranchRestriction>(overrideUrl, parameters.ToDictionary(), pageLen);
        }

#if CS_8
        internal IAsyncEnumerable<BranchRestriction> EnumerateBranchRestrictionsAsync(
            string accountName,
            string slug,
            BranchRestrictionsParameters parameters,
            int? pageLen,
            CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<BranchRestriction>(
                overrideUrl,
                parameters.ToDictionary(),
                pageLen,
                token);
        }
#endif

        internal BranchRestriction PostBranchRestriction(string accountName, string slug, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        internal async Task<BranchRestriction> PostBranchRestrictionAsync(string accountName, string slug, BranchRestriction restriction, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return await _sharpBucketV2.PostAsync(restriction, overrideUrl, token: token);
        }

        internal async Task<BranchRestriction> BranchRestrictionAsync(string accountName, string slug, BranchRestriction restriction, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return await _sharpBucketV2.PostAsync(restriction, overrideUrl, token: token);
        }

        internal BranchRestriction GetBranchRestriction(string accountName, string slug, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            return _sharpBucketV2.Get<BranchRestriction>(overrideUrl);
        }

        internal async Task<BranchRestriction> GetBranchRestrictionAsync(string accountName, string slug, int restrictionId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            return await _sharpBucketV2.GetAsync<BranchRestriction>(overrideUrl, token: token);
        }

        internal BranchRestriction PutBranchRestriction(string accountName, string slug, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restriction.id}");
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        internal async Task<BranchRestriction> PutBranchRestrictionAsync(string accountName, string slug, BranchRestriction restriction, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restriction.id}");
            return await _sharpBucketV2.PutAsync(restriction, overrideUrl, token: token);
        }

        internal void DeleteBranchRestriction(string accountName, string slug, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            _sharpBucketV2.Delete(overrideUrl);
        }

        internal async Task DeleteBranchRestrictionAsync(string accountName, string slug, int restrictionId, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            await _sharpBucketV2.DeleteAsync(overrideUrl, token: token);
        }

        #endregion

        #region Diff resource

        internal string GetDiff(string accountName, string slug, string spec, DiffParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "diff/" + spec);
            return _sharpBucketV2.Get(overrideUrl, parameters.ToDictionary());
        }

        internal async Task<string> GetDiffAsync(string accountName, string slug, string spec, DiffParameters parameters, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "diff/" + spec);
            return await _sharpBucketV2.GetAsync(overrideUrl, parameters.ToDictionary(), token: token);
        }

        internal string GetPatch(string accountName, string slug, string spec)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "patch/" + spec);
            return _sharpBucketV2.Get(overrideUrl);
        }

        internal async Task<string> GetPatchAsync(string accountName, string slug, string spec, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "patch/" + spec);
            return await _sharpBucketV2.GetAsync(overrideUrl, token: token);
        }

        #endregion

        #region Commits Resource

        internal List<Commit> ListCommits(string accountName, string slug, string branchOrTag, ListCommitsParameters commitsParameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "commits/");
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return GetPaginatedValues<Commit>(overrideUrl, commitsParameters?.Max ?? 0, commitsParameters?.ToDictionary());
        }

        internal IEnumerable<Commit> EnumerateCommits(string accountName, string slug, string branchOrTag, EnumerateCommitsParameters commitsParameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "commits/");
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return _sharpBucketV2.EnumeratePaginatedValues<Commit>(overrideUrl, commitsParameters.ToDictionary(), commitsParameters.PageLen);
        }
#if CS_8
        internal IAsyncEnumerable<Commit> EnumerateCommitsAsync(string accountName, string slug, string branchOrTag, EnumerateCommitsParameters commitsParameters, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "commits/");
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Commit>(overrideUrl, commitsParameters.ToDictionary(), commitsParameters.PageLen, token);
        }
#endif

        internal Commit GetCommit(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}");
            return _sharpBucketV2.Get<Commit>(overrideUrl);
        }

        internal async Task<Commit> GetCommitAsync(string accountName, string slug, string revision, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}");
            return await _sharpBucketV2.GetAsync<Commit>(overrideUrl, token: token);
        }

        internal UserRole ApproveCommit(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            return _sharpBucketV2.Post<UserRole>(null, overrideUrl);
        }

        internal async Task<UserRole> ApproveCommitAsync(string accountName, string slug, string revision, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            return await _sharpBucketV2.PostAsync<UserRole>(null, overrideUrl, token: token);
        }

        internal void DeleteCommitApproval(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            _sharpBucketV2.Delete(overrideUrl);
        }

        internal async Task DeleteCommitApprovalAsync(string accountName, string slug, string revision, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            await _sharpBucketV2.DeleteAsync(overrideUrl, token: token);
        }

        internal BuildInfo AddNewBuildStatus(string accountName, string slug, string revision, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/");
            return _sharpBucketV2.Post(buildInfo, overrideUrl);
        }

        internal async Task<BuildInfo> AddNewBuildStatusAsync(string accountName, string slug, string revision, BuildInfo buildInfo, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/");
            return await _sharpBucketV2.PostAsync(buildInfo, overrideUrl, token: token);
        }

        internal BuildInfo GetBuildStatusInfo(string accountName, string slug, string revision, string key)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/" + key);
            return _sharpBucketV2.Get<BuildInfo>(overrideUrl);
        }

        internal async Task<BuildInfo> GetBuildStatusInfoAsync(string accountName, string slug, string revision, string key, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/" + key);
            return await _sharpBucketV2.GetAsync<BuildInfo>(overrideUrl, token: token);
        }

        internal BuildInfo ChangeBuildStatusInfo(string accountName, string slug, string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/{key}");
            return _sharpBucketV2.Put(buildInfo, overrideUrl);
        }

        internal async Task<BuildInfo> ChangeBuildStatusInfoAsync(string accountName, string slug, string revision, string key, BuildInfo buildInfo, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/{key}");
            return await _sharpBucketV2.PutAsync(buildInfo, overrideUrl, token: token);
        }
        #endregion

        #region Default Reviewer Resource

        internal void PutDefaultReviewer(string accountName, string slug, string targetUsername)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"default-reviewers/{targetUsername}");
            _sharpBucketV2.Put(new object(), overrideUrl);
        }

        internal async Task PutDefaultReviewerAsync(string accountName, string slug, string targetUsername, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"default-reviewers/{targetUsername}");
            await _sharpBucketV2.PutAsync(new object(), overrideUrl, token: token);
        }

        #endregion

        #region Branch Resource

        /// <summary>
        /// Manage branches for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/branches
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        /// <returns></returns>
        public BranchResource BranchResource(string accountName, string repoSlugOrName)
        {
            return new BranchResource(accountName, repoSlugOrName, this);
        }

        internal List<Branch> ListBranches(string accountName, string slug, ListParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/branches/");
            return GetPaginatedValues<Branch>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        internal IEnumerable<Branch> EnumerateBranches(string accountName, string slug, EnumerateParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/branches/");
            return _sharpBucketV2.EnumeratePaginatedValues<Branch>(overrideUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Branch> EnumerateBranchesAsync(string accountName, string slug, EnumerateParameters parameters, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/branches/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Branch>(overrideUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        internal void DeleteBranch(string accountName, string repSlug, string branchName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repSlug, "refs/branches/" + branchName);
            _sharpBucketV2.Delete(overrideUrl);
        }

        internal async Task DeleteBranchAsync(string accountName, string repSlug, string branchName, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repSlug, "refs/branches/" + branchName);
            await _sharpBucketV2.DeleteAsync(overrideUrl, token: token);
        }

        #endregion

        #region Tag Resource

        /// <summary>
        /// Manage tags for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/tags
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        [Obsolete("Use RepositoryResource(string,string).TagsResource instead")]
        public TagResource TagResource(string accountName, string repoSlugOrName)
        {
            return new TagResource(accountName, repoSlugOrName, this);
        }

        internal List<Tag> ListTags(string accountName, string slug, ListParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/tags/");
            return GetPaginatedValues<Tag>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        internal IEnumerable<Tag> EnumerateTags(string accountName, string slug, EnumerateParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/tags/");
            return _sharpBucketV2.EnumeratePaginatedValues<Tag>(overrideUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        internal IAsyncEnumerable<Tag> EnumerateTagsAsync(string accountName, string slug, EnumerateParameters parameters, CancellationToken token)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/tags/");
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Tag>(overrideUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        #endregion

        #region Src Resource

        internal List<TreeEntry> ListTreeEntries(string srcResourcePath, string subDirPath, ListParameters listParameters)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subDirPath);
            return listParameters == null
                ? GetPaginatedValues<TreeEntry>(overrideUrl)
                : GetPaginatedValues<TreeEntry>(overrideUrl, listParameters.Max, listParameters.ToDictionary());
        }

        internal IEnumerable<TreeEntry> EnumerateTreeEntries(
            string srcResourcePath, string subDirPath, EnumerateParameters parameters)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subDirPath);
            return _sharpBucketV2.EnumeratePaginatedValues<TreeEntry>(
                overrideUrl, parameters?.ToDictionary(), parameters?.PageLen);
        }

#if CS_8
        internal IAsyncEnumerable<TreeEntry> EnumerateTreeEntriesAsync(
            string srcResourcePath, string subDirPath, EnumerateParameters parameters, CancellationToken token)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subDirPath);
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<TreeEntry>(
                overrideUrl, parameters?.ToDictionary(), parameters?.PageLen, token);
        }
#endif

        internal TreeEntry GetTreeEntry(string srcResourcePath, string subPath = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subPath);
            return _sharpBucketV2.Get<TreeEntry>(overrideUrl, new { format = "meta" });
        }

        internal Task<TreeEntry> GetTreeEntryAsync(string srcResourcePath, CancellationToken token)
        {
            return GetTreeEntryAsync(srcResourcePath, null, token);
        }

        internal async Task<TreeEntry> GetTreeEntryAsync(string srcResourcePath, string subPath, CancellationToken token)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subPath);
            return await _sharpBucketV2.GetAsync<TreeEntry>(overrideUrl, new { format = "meta" }, token);
        }

        internal Uri GetSrcRootRedirectLocation(string srcResourcePath)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath);
            return _sharpBucketV2.GetRedirectLocation(overrideUrl, new { format = "meta" });
        }

        internal Task<Uri> GetSrcRootRedirectLocationAsync(string srcResourcePath, CancellationToken token)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath);
            return _sharpBucketV2.GetRedirectLocationAsync(overrideUrl, new { format = "meta" }, token);
        }

        internal string GetFileContent(string srcResourcePath, string filePath)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, filePath);
            return _sharpBucketV2.Get(overrideUrl);
        }

        internal async Task<string> GetFileContentAsync(string srcResourcePath, string filePath, CancellationToken token)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, filePath);
            return await _sharpBucketV2.GetAsync(overrideUrl, token);
        }

        #endregion
    }
}