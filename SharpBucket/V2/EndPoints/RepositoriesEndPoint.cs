using System;
using System.Collections.Generic;
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

        internal Repository PutRepository(Repository repo, string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        internal Repository PostRepository(Repository repo, string accountName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name, null);
            return _sharpBucketV2.Post(repo, overrideUrl);
        }

        internal Repository DeleteRepository(string accountName, string repository)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(new Repository(), overrideUrl);
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

        internal List<Fork> ListForks(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return GetPaginatedValues<Fork>(overrideUrl, max);
        }

        #endregion

        #region Pull Requests Resource

        public PullRequestsResource PullReqestsResource(string accountName, string repository)
        {
            return new PullRequestsResource(accountName, repository, this);
        }

        internal List<PullRequest> ListPullRequests(string accountName, string repository, int max)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return GetPaginatedValues<PullRequest>(overrideUrl, max);
        }

        internal PullRequest PostPullRequest(string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        internal PullRequest PutPullRequest(string accountName, string repository, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }

        internal List<Activity> GetPullRequestLog(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        #endregion

        #region Pull Request Resource

        internal PullRequest GetPullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
            return _sharpBucketV2.Get(new PullRequest(), overrideUrl);
        }

        internal List<Commit> ListPullRequestCommits(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        internal PullRequestInfo ApprovePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Post(new PullRequestInfo(), overrideUrl);
        }

        internal object RemovePullRequestApproval(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Delete(new PullRequestInfo(), overrideUrl);
        }

        internal object GetDiffForPullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
            return _sharpBucketV2.Get(new Object(), overrideUrl);
        }

        internal List<Activity> GetPullRequestActivity(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        internal Merge AcceptAndMergePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
            return _sharpBucketV2.Post(new Merge(), overrideUrl);
        }

        internal Merge DeclinePullRequest(string accountName, string repository, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
            return _sharpBucketV2.Get(new Merge(), overrideUrl);
        }

        internal List<Comment> ListPullRequestComments(string accountName, string repository, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        internal Comment GetPullRequestComment(string accountName, string repository, int pullRequestId, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
            return _sharpBucketV2.Get(new Comment(), overrideUrl);
        }

        #endregion

        #region Branch Restrictions resource

        internal List<BranchRestriction> ListBranchRestrictions(string accountName, string repository, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return GetPaginatedValues<BranchRestriction>(overrideUrl, max);
        }

        internal BranchRestriction PostBranchRestriction(string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        internal BranchRestriction GetBranchRestriction(string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(new BranchRestriction(), overrideUrl);
        }

        internal BranchRestriction PutBranchRestriction(string accountName, string repository, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restriction.id);
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        internal BranchRestriction DeleteBranchRestriction(string accountName, string repository, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(new BranchRestriction(), overrideUrl);
        }

        #endregion

        #region Diff resource

        internal object GetDiff(string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        internal object GetPatch(string accountName, string repository, object options)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
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

        internal Commit GetCommit(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision);
            return _sharpBucketV2.Get(new Commit(), overrideUrl);
        }

        internal List<Comment> ListCommitComments(string accountName, string repository, string revision, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        internal object GetCommitComment(string accountName, string repository, string revision, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/" + revision + "/" + commentId + "/");
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        internal object ApproveCommit(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Post(new object(), overrideUrl);
        }

        internal object DeleteCommitApproval(string accountName, string repository, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Delete(new object(), overrideUrl);
        }

        internal object AddNewBuildStatus(string accountName, string repository, string revision, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/");
            return _sharpBucketV2.Post(buildInfo, overrideUrl);
        }

        internal BuildInfo GetBuildStatusInfo(string accountName, string repository, string revision, string key)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Get(new BuildInfo(), overrideUrl);
        }

        internal object ChangeBuildStatusInfo(string accountName, string repository, string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision + "/statuses/build/" + key);
            return _sharpBucketV2.Put(buildInfo, overrideUrl);
        }

        #endregion

        #region Default Reviewer Resource

        internal object PutDefaultReviewer(string accountName, string repository, string targetUsername)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repository, "default-reviewers/" + targetUsername);
            return _sharpBucketV2.Put(new object(), overrideUrl);
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

        #endregion
    }
}