using System;
using System.Collections.Generic;
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
        /// <returns></returns>
        public List<Repository> ListRepositories(string accountName) => ListRepositories(accountName, new ListParameters());

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories. 
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(string accountName, ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var overrideUrl = $"{_baseUrl}{accountName.GuidOrValue()}/";
            return GetPaginatedValues<Repository>(overrideUrl, parameters.Max, parameters.ToDictionary());
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

        internal Repository PutRepository(Repository repo, string accountName, string slug)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        internal Repository PostRepository(Repository repo, string accountName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repo.name.ToSlug(), null);
            return _sharpBucketV2.Post(repo, overrideUrl);
        }

        internal Repository DeleteRepository(string accountName, string slug)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, null);
            return _sharpBucketV2.Delete<Repository>(overrideUrl);
        }

        private string GetRepositoryUrl(string accountName, string slug, string append)
        {
            return $"{_baseUrl}{accountName}/{slug}/{append}";
        }

        internal List<Watcher> ListWatchers(string accountName, string slug, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "watchers");
            return GetPaginatedValues<Watcher>(overrideUrl, max);
        }

        internal List<Fork> ListForks(string accountName, string slug, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "forks");
            return GetPaginatedValues<Fork>(overrideUrl, max);
        }

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

        internal List<PullRequest> ListPullRequests(string accountName, string slug, ListParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return GetPaginatedValues<PullRequest>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        internal PullRequest PostPullRequest(string accountName, string slug, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        internal PullRequest PutPullRequest(string accountName, string slug, PullRequest pullRequest)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }

        internal List<Activity> GetPullRequestLog(string accountName, string slug, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "pullrequests/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        #endregion

        #region Pull Request Resource

        internal PullRequest GetPullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/");
            return _sharpBucketV2.Get<PullRequest>(overrideUrl);
        }

        internal List<Commit> ListPullRequestCommits(string accountName, string slug, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/commits/");
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        internal PullRequestInfo ApprovePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            return _sharpBucketV2.Post<PullRequestInfo>(null, overrideUrl);
        }

        internal PullRequestInfo RemovePullRequestApproval(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/approve/");
            return _sharpBucketV2.Delete<PullRequestInfo>(overrideUrl);
        }

        internal object GetDiffForPullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/diff/");
            return _sharpBucketV2.Get(overrideUrl);
        }

        internal List<Activity> GetPullRequestActivity(string accountName, string slug, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/activity/");
            return GetPaginatedValues<Activity>(overrideUrl, max);
        }

        internal Merge AcceptAndMergePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/merge/");
            return _sharpBucketV2.Post<Merge>(null, overrideUrl);
        }

        internal PullRequest DeclinePullRequest(string accountName, string slug, int pullRequestId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/decline/");
            return _sharpBucketV2.Post<PullRequest>(null, overrideUrl);
        }

        internal List<Comment> ListPullRequestComments(string accountName, string slug, int pullRequestId, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        internal Comment GetPullRequestComment(string accountName, string slug, int pullRequestId, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/comments/{commentId}/");
            return _sharpBucketV2.Get<Comment>(overrideUrl);
        }

        internal Comment PostPullRequestComment(string accountName, string slug, int pullRequestId, Comment comment)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"pullrequests/{pullRequestId}/comments/");
            return _sharpBucketV2.Post(comment, overrideUrl);
        }

        #endregion

        #region Branch Restrictions resource

        internal List<BranchRestriction> ListBranchRestrictions(string accountName, string slug, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return GetPaginatedValues<BranchRestriction>(overrideUrl, max);
        }

        internal BranchRestriction PostBranchRestriction(string accountName, string slug, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        internal BranchRestriction GetBranchRestriction(string accountName, string slug, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            return _sharpBucketV2.Get<BranchRestriction>(overrideUrl);
        }

        internal BranchRestriction PutBranchRestriction(string accountName, string slug, BranchRestriction restriction)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restriction.id}");
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        internal BranchRestriction DeleteBranchRestriction(string accountName, string slug, int restrictionId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"branch-restrictions/{restrictionId}");
            return _sharpBucketV2.Delete<BranchRestriction>(overrideUrl);
        }

        #endregion

        #region Diff resource

        internal string GetDiff(string accountName, string slug, string spec, DiffParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "diff/" + spec);
            return _sharpBucketV2.Get(overrideUrl, parameters.ToDictionary());
        }

        internal string GetPatch(string accountName, string slug, string spec)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "patch/" + spec);
            return _sharpBucketV2.Get(overrideUrl);
        }

        #endregion

        #region Commits Resource

        internal List<Commit> ListCommits(string accountName, string slug, string branchOrTag = null, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "commits/");
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }
            return GetPaginatedValues<Commit>(overrideUrl, max);
        }

        internal Commit GetCommit(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}");
            return _sharpBucketV2.Get<Commit>(overrideUrl);
        }

        internal List<Comment> ListCommitComments(string accountName, string slug, string revision, int max = 0)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commits/{revision}/comments/");
            return GetPaginatedValues<Comment>(overrideUrl, max);
        }

        internal Comment GetCommitComment(string accountName, string slug, string revision, int commentId)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commits/{revision}/comments/{revision}/{commentId}/");
            return _sharpBucketV2.Get<Comment>(overrideUrl);
        }

        internal UserRole ApproveCommit(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            return _sharpBucketV2.Post<UserRole>(null, overrideUrl);
        }

        internal void DeleteCommitApproval(string accountName, string slug, string revision)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/approve/");
            _sharpBucketV2.Delete<object>(overrideUrl);
        }

        internal BuildInfo AddNewBuildStatus(string accountName, string slug, string revision, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/");
            return _sharpBucketV2.Post(buildInfo, overrideUrl);
        }

        internal BuildInfo GetBuildStatusInfo(string accountName, string slug, string revision, string key)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/" + key);
            return _sharpBucketV2.Get<BuildInfo>(overrideUrl);
        }

        internal BuildInfo ChangeBuildStatusInfo(string accountName, string slug, string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"commit/{revision}/statuses/build/{key}");
            return _sharpBucketV2.Put(buildInfo, overrideUrl);
        }

        #endregion

        #region Default Reviewer Resource

        internal void PutDefaultReviewer(string accountName, string slug, string targetUsername)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, $"default-reviewers/{targetUsername}");
            _sharpBucketV2.Put(new object(), overrideUrl);
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

        internal Branch DeleteBranch(string accountName, string repSlug, string branchName)
        {
            var overrideUrl = GetRepositoryUrl(accountName, repSlug, "refs/branches/" + branchName);
            return _sharpBucketV2.Delete<Branch>(overrideUrl);
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
        /// <returns></returns>
        public TagResource TagResource(string accountName, string repoSlugOrName)
        {
            return new TagResource(accountName, repoSlugOrName, this);
        }

        internal List<Tag> ListTags(string accountName, string slug, ListParameters parameters)
        {
            var overrideUrl = GetRepositoryUrl(accountName, slug, "refs/tags/");
            return GetPaginatedValues<Tag>(overrideUrl, parameters.Max, parameters.ToDictionary());
        }

        #endregion

        #region Src Resource

        internal List<TreeEntry> ListTreeEntries(string srcResourcePath, string subDirPath = null, ListParameters listParameters = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subDirPath);
            return listParameters == null
                ? GetPaginatedValues<TreeEntry>(overrideUrl)
                : GetPaginatedValues<TreeEntry>(overrideUrl, listParameters.Max, listParameters.ToDictionary());
        }

        internal TreeEntry GetTreeEntry(string srcResourcePath, string subPath = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, subPath);
            return _sharpBucketV2.Get<TreeEntry>(overrideUrl, new { format = "meta" });
        }

        internal string GetFileContent(string srcResourcePath, string filePath)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath, filePath);
            return _sharpBucketV2.Get(overrideUrl);
        }

        #endregion
    }
}