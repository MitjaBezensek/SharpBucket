using System;
using System.Collections.Generic;
using Serilog;
using SharpBucket.V2.Pocos;
using Comment = SharpBucket.V2.Pocos.Comment;
using Repository = SharpBucket.V2.Pocos.Repository;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The repositories endpoint has a number of resources you can use to manage repository resources. 
    /// For all repository resources, you supply a  repo_slug that identifies the specific repository.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint
    /// </summary>
    public class RepositoriesEndPoint : EndPoint
    {
        #region Repositories End Point

        public RepositoriesEndPoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "repositories/")
        {
        }

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories. 
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(string accountName, int max = 0)
        {
            var overrideUrl = _baseUrl + accountName + "/";
            return GetPaginatedValues<Repository>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging.
        /// List of repositories associated with an account and project. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories. 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(ILogger logger, string accountName, int max = 0)
        {
            var overrideUrl = _baseUrl + accountName + "/";
            return GetPaginatedValues<Repository>(logger, overrideUrl, max);
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.  This produces a paginated response. 
        /// Pagination only goes forward (it's not possible to navigate to previous pages) and navigation is done by following the URL for the next page.
        /// The returned repositories are ordered by creation date, oldest repositories first. Only public repositories are returned.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(int max = 0)
        {
            return GetPaginatedValues<Repository>(_baseUrl, max);
        }

        /// <summary>
        /// With Logging.
        /// List of all the public repositories on Bitbucket.  This produces a paginated response. 
        /// Pagination only goes forward (it's not possible to navigate to previous pages) and navigation is done by following the URL for the next page.
        /// The returned repositories are ordered by creation date, oldest repositories first. Only public repositories are returned.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(ILogger logger, int max = 0)
        {
            return GetPaginatedValues<Repository>(logger, _baseUrl, max);
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
        /// <param name="repository">The repository slug.</param>
        /// <returns></returns>
        public RepositoryResource RepositoryResource(string accountName, string repository)
        {
            return new RepositoryResource(accountName, repository, this);
        }

        internal Repository GetRepository(string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Get(new Repository(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        internal Repository GetRepository(ILogger logger, string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Get(logger, new Repository(), overrideUrl);
        }

        internal Repository PutRepository(Repository repo, string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repo"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        internal Repository PutRepository(ILogger logger, Repository repo, string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        internal Repository PostRepository(Repository repo, string accountName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name, null);
            return _sharpBucketV2.Post(repo, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repo"></param>
        /// <param name="accountName"></param>
        /// <returns></returns>
        internal Repository PostRepository(ILogger logger, Repository repo, string accountName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name, null);
            return _sharpBucketV2.Post(logger, repo, overrideUrl);
        }

        internal Repository DeleteRepository(string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(new Repository(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        internal Repository DeleteRepository(ILogger logger, string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(logger, new Repository(), overrideUrl);
        }


        private string GetRepositoryUrl(string accountName, string repository, string append)
        {
            var format = _baseUrl + "{0}/{1}/{2}";
            return string.Format(format, accountName, repository, append);
        }

        internal List<Watcher> ListWatchers(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
            return GetPaginatedValues<Watcher>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Watcher> ListWatchers(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
            return GetPaginatedValues<Watcher>(logger, overrideUrl, max);
        }

        internal List<Fork> ListForks(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return GetPaginatedValues<Fork>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Fork> ListForks(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return GetPaginatedValues<Fork>(logger, overrideUrl, max);
        }

        #endregion

        #region Pull Requests Resource

        public PullRequestsResource PullRequestsResource(string accountName, string repository)
        {
            return new PullRequestsResource(accountName, repository, this);
        }

        internal List<PullRequest> ListPullRequests(string accountName, string repository, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return GetPaginatedValues<PullRequest>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<PullRequest> ListPullRequests(ILogger logger, string accountName, string repository, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return GetPaginatedValues<PullRequest>(logger, overrideUrl, max);
        }

        internal PullRequest PostPullRequest(string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequest"></param>
        /// <returns></returns>
        internal PullRequest PostPullRequest(ILogger logger, string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(logger, pullRequest, overrideUrl);
        }

        internal PullRequest PutPullRequest(string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequest"></param>
        /// <returns></returns>
        internal PullRequest PutPullRequest(ILogger logger, string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(logger, pullRequest, overrideUrl);
        }

        internal List<Activity> GetPullRequestLog(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Activity> GetPullRequestLog(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
            return GetPaginatedValues<Activity>(logger, overrideUrl, max);
        }

        #endregion

        #region Pull Request Resource

        internal PullRequest GetPullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
            return _sharpBucketV2.Get(new PullRequest(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal PullRequest GetPullRequest(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
            return _sharpBucketV2.Get(logger, new PullRequest(), overrideUrl);
        }


        internal List<Commit> ListPullRequestCommits(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Commit> ListPullRequestCommits(ILogger logger, string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
            return GetPaginatedValues<Commit>(logger, overrideUrl, max);
        }


        internal PullRequestInfo ApprovePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Post(new PullRequestInfo(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal PullRequestInfo ApprovePullRequest(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Post(logger, new PullRequestInfo(), overrideUrl);
        }

        internal object RemovePullRequestApproval(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Delete(new PullRequestInfo(), overrideUrl);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal object RemovePullRequestApproval(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Delete(logger, new PullRequestInfo(), overrideUrl);
        }

        internal object GetDiffForPullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
            return _sharpBucketV2.Get(new Object(), overrideUrl);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal object GetDiffForPullRequest(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
            return _sharpBucketV2.Get(logger, new Object(), overrideUrl);
        }

        internal List<Activity> GetPullRequestActivity(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Activity> GetPullRequestActivity(ILogger logger, string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
            return GetPaginatedValues<Activity>(logger, overrideUrl, max);
        }

        internal Merge AcceptAndMergePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
            return _sharpBucketV2.Post(new Merge(), overrideUrl);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal Merge AcceptAndMergePullRequest(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
            return _sharpBucketV2.Post(logger, new Merge(), overrideUrl);
        }

        internal Merge DeclinePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
            return _sharpBucketV2.Get(new Merge(), overrideUrl);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <returns></returns>
        internal Merge DeclinePullRequest(ILogger logger, string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
            return _sharpBucketV2.Get(logger, new Merge(), overrideUrl);
        }

        internal List<Comment> ListPullRequestComments(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Comment> ListPullRequestComments(ILogger logger, string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
            return GetPaginatedValues<Comment>(logger, overrideUrl, max);
        }

        internal Comment GetPullRequestComment(string accountName, string repository, int pullRequestId, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
            return _sharpBucketV2.Get(new Comment(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="pullRequestId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        internal Comment GetPullRequestComment(ILogger logger, string accountName, string repository, int pullRequestId, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
            return _sharpBucketV2.Get(logger, new Comment(), overrideUrl);
        }

        #endregion

        #region Branch Restrictions resource

        internal List<BranchRestriction> ListBranchRestrictions(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return GetPaginatedValues<BranchRestriction>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<BranchRestriction> ListBranchRestrictions(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return GetPaginatedValues<BranchRestriction>(logger, overrideUrl, max);
        }

        internal BranchRestriction PostBranchRestriction(string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="restriction"></param>
        /// <returns></returns>
        internal BranchRestriction PostBranchRestriction(ILogger logger, string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(logger, restriction, overrideUrl);
        }

        internal BranchRestriction GetBranchRestriction(string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(new BranchRestriction(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="restrictionId"></param>
        /// <returns></returns>
        internal BranchRestriction GetBranchRestriction(ILogger logger, string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(logger, new BranchRestriction(), overrideUrl);
        }

        internal BranchRestriction PutBranchRestriction(string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restriction.id);
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="restriction"></param>
        /// <returns></returns>
        internal BranchRestriction PutBranchRestriction(ILogger logger, string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restriction.id);
            return _sharpBucketV2.Put(logger, restriction, overrideUrl);
        }
        internal BranchRestriction DeleteBranchRestriction(string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(new BranchRestriction(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="restrictionId"></param>
        /// <returns></returns>
        internal BranchRestriction DeleteBranchRestriction(ILogger logger, string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(logger, new BranchRestriction(), overrideUrl);
        }

        #endregion

        #region Diff resource

        internal object GetDiff(string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal object GetDiff(ILogger logger, string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(logger, new object(), overrideUrl);
        }

        internal object GetPatch(string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal object GetPatch(ILogger logger, string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(logger, new object(), overrideUrl);
        }

        #endregion

        #region Commits Resource

        internal List<Commit> ListCommits(string accountName, string repository, string branchortag = null, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
            if (!string.IsNullOrEmpty(branchortag))
            {
                overrideUrl += branchortag;
            }
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="branchortag"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Commit> ListCommits(ILogger logger, string accountName, string repository, string branchortag = null, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
            if (!string.IsNullOrEmpty(branchortag))
            {
                overrideUrl += branchortag;
            }
            return GetPaginatedValues<Commit>(logger, overrideUrl, max);
        }

        internal Commit GetCommit(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision);
            return _sharpBucketV2.Get(new Commit(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        internal Commit GetCommit(ILogger logger, string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision);
            return _sharpBucketV2.Get(logger, new Commit(), overrideUrl);
        }

        internal List<Comment> ListCommitComments(string accountName, string repository, string revision, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Comment> ListCommitComments(ILogger logger, string accountName, string repository, string revision, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/");
            return GetPaginatedValues<Comment>(logger, overrideUrl, max);
        }

        internal object GetCommitComment(string accountName, string repository, string revision, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/" + revision + "/" + commentId + "/");
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        internal object GetCommitComment(ILogger logger, string accountName, string repository, string revision, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/" + revision + "/" + commentId + "/");
            return _sharpBucketV2.Get(logger, new object(), overrideUrl);
        }

        internal object ApproveCommit(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Post(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        internal object ApproveCommit(ILogger logger, string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Post(logger, new object(), overrideUrl);
        }

        internal object DeleteCommitApproval(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Delete(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        internal object DeleteCommitApproval(ILogger logger, string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Delete(logger, new object(), overrideUrl);
        }

        internal object AddNewBuildStatus(string accountName, string repository, string revision, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/");
            return _sharpBucketV2.Post(buildInfo, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <param name="buildInfo"></param>
        /// <returns></returns>
        internal object AddNewBuildStatus(ILogger logger, string accountName, string repository, string revision, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/");
            return _sharpBucketV2.Post(logger, buildInfo, overrideUrl);
        }

        internal BuildInfo GetBuildStatusInfo(string accountName, string repository, string revision, string key)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Get(new BuildInfo(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        internal BuildInfo GetBuildStatusInfo(ILogger logger, string accountName, string repository, string revision, string key)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Get(logger, new BuildInfo(), overrideUrl);
        }

        internal object ChangeBuildStatusInfo(string accountName, string repository, string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Put(buildInfo, overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="revision"></param>
        /// <param name="key"></param>
        /// <param name="buildInfo"></param>
        /// <returns></returns>
        internal object ChangeBuildStatusInfo(ILogger logger, string accountName, string repository, string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Put(logger, buildInfo, overrideUrl);
        }

        #endregion

        #region Default Reviewer Resource

        internal object PutDefaultReviewer(string accountName, string repository, string targetUsername)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "default-reviewers/" + targetUsername);
            return _sharpBucketV2.Put(new object(), overrideUrl);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="targetUsername"></param>
        /// <returns></returns>
        internal object PutDefaultReviewer(ILogger logger, string accountName, string repository, string targetUsername)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "default-reviewers/" + targetUsername);
            return _sharpBucketV2.Put(logger, new object(), overrideUrl);
        }

        #endregion

        #region Branch Resource

        public BranchResource BranchResource(string accountName, string repository)
        {
            return new BranchResource(accountName, repository, this);
        }

        internal List<Branch> ListBranches(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "refs/branches/");
            return GetPaginatedValues<Branch>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Branch> ListBranches(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "refs/branches/");
            return GetPaginatedValues<Branch>(logger, overrideUrl, max);
        }

        #endregion

        #region Tag Resource

        public TagResource TagResource(string accountName, string repository)
        {
            return new TagResource(accountName, repository, this);
        }

        internal List<Tag> ListTags(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "refs/tags/");
            return GetPaginatedValues<Tag>(overrideUrl, max);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal List<Tag> ListTags(ILogger logger, string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "refs/tags/");
            return GetPaginatedValues<Tag>(logger, overrideUrl, max);
        }

        #endregion
    }
}