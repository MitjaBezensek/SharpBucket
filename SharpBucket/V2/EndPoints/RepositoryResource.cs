using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Use this resource to get information associated with an individual repository. 
    /// You can use these calls with public or private repositories. 
    /// Private repositories require the caller to authenticate with an account that has the appropriate authorization.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
    /// </summary>
    public class RepositoryResource : EndPoint
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _accountName;
        private readonly string _slug;

        #region Repository Resource

        public RepositoryResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
            : base(repositoriesEndPoint, $"{accountName.GuidOrValue()}/{repoSlugOrName.ToSlug()}/")
        {
            _slug = repoSlugOrName.ToSlug();
            _accountName = accountName.GuidOrValue();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Returns a single repository.
        /// </summary>
        /// <returns></returns>
        public Repository GetRepository()
        {
            return _sharpBucketV2.Get<Repository>(_baseUrl);
        }

        /// <summary>
        /// Returns a single repository.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<Repository> GetRepositoryAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync<Repository>(_baseUrl, token);
        }

        /// <summary>
        /// Removes a repository.  
        /// </summary>
        /// <returns></returns>
        public void DeleteRepository()
        {
            _sharpBucketV2.Delete(_baseUrl);
        }

        /// <summary>
        /// Removes a repository.  
        /// </summary>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task DeleteRepositoryAsync(CancellationToken token = default)
        {
            return _sharpBucketV2.DeleteAsync(_baseUrl, token);
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repository">The repository to create.</param>
        /// <returns>The created repository.</returns>
        public Repository PostRepository(Repository repository)
        {
            return _sharpBucketV2.Post(repository, _baseUrl);
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repository">The repository to create.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The created repository.</returns>
        public Task<Repository> PostRepositoryAsync(Repository repository, CancellationToken token = default)
        {
            return _sharpBucketV2.PostAsync(repository, _baseUrl, token);
        }

        /// <summary>
        /// List accounts watching a repository. 
        /// </summary>
        /// <returns></returns>
        public List<UserShort> ListWatchers()
        {
            return ListWatchers(0);
        }

        /// <summary>
        /// List accounts watching a repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<UserShort> ListWatchers(int max)
        {
            return _sharpBucketV2.GetPaginatedValues<UserShort>(_baseUrl + "watchers", max);
        }

        /// <summary>
        /// Enumerate accounts watching a repository.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        public IEnumerable<UserShort> EnumerateWatchers(int? pageLen = null)
        {
            return _sharpBucketV2.EnumeratePaginatedValues<UserShort>(_baseUrl + "watchers", null, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate accounts watching a repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<UserShort> EnumerateWatchersAsync(int? pageLen = null, CancellationToken token = default)
        {
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<UserShort>(_baseUrl + "watchers", null, pageLen, token);
        }
#endif

        /// <summary>
        /// List repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListForks()
        {
            return ListForks(0);
        }

        /// <summary>
        /// List repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<Repository> ListForks(int max)
        {
            return GetPaginatedValues<Repository>(_baseUrl + "forks", max);
        }

        /// <summary>
        /// Enumerate repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Repository> EnumerateForks(int? pageLen = null)
        {
            return _sharpBucketV2.EnumeratePaginatedValues<Repository>(_baseUrl + "forks", pageLen: pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate repository forks asynchronously, doing requests page by page.
        /// This call returns a repository object for each fork.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Repository> EnumerateForksAsync(int? pageLen = null, CancellationToken token = default)
        {
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(_baseUrl + "forks", new Dictionary<string, object>(), pageLen, token);
        }
#endif

        #endregion

        #region BranchResource

        private BranchResource _branchesResource;

        public BranchResource BranchesResource => this._branchesResource ??
                                                (_branchesResource = new BranchResource(_accountName, _slug, _repositoriesEndPoint));

        #endregion

        #region Pull Requests Resource

        /// <summary>
        /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
        /// This resource allows you to manage the attributes of a pull request also. For example, you can list the commits 
        /// or reviewers associated with a pull request. You can also accept or decline a pull request with this resource. 
        /// Finally, you can use this resource to manage the comments on a pull request as well.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/pullrequests+Resource
        /// </summary>
        /// <returns></returns>
        public PullRequestsResource PullRequestsResource()
        {
            return new PullRequestsResource(_accountName, _slug, _repositoriesEndPoint);
        }

        #endregion

        #region Branch Restrictions Resource

        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/branch-restrictions#get
        /// <summary>
        /// List the information associated with a repository's branch restrictions. 
        /// </summary>
        public List<BranchRestriction> ListBranchRestrictions()
            => ListBranchRestrictions(new ListBranchRestrictionsParameters());

        /// <summary>
        /// List the information associated with a repository's branch restrictions. 
        /// </summary>
        /// <param name="parameters">Query parameters that can be used to filter the results.</param>
        public List<BranchRestriction> ListBranchRestrictions(
            ListBranchRestrictionsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<BranchRestriction>(_baseUrl + "branch-restrictions", parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate the information associated with a repository's branch restrictions.
        /// Requests will be done page by page while enumerating.
        /// </summary>
        public IEnumerable<BranchRestriction> EnumerateBranchRestrictions()
            => EnumerateBranchRestrictions(new EnumerateBranchRestrictionsParameters());

        /// <summary>
        /// Enumerate the information associated with a repository's branch restrictions.
        /// Requests will be done page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Query parameters that can be used to filter the results.</param>
        public IEnumerable<BranchRestriction> EnumerateBranchRestrictions(
            EnumerateBranchRestrictionsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<BranchRestriction>(
                _baseUrl + "branch-restrictions",
                parameters.ToDictionary(),
                parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the information associated with a repository's branch restrictions asynchronously,
        /// doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<BranchRestriction> EnumerateBranchRestrictionsAsync(
            CancellationToken token = default)
            => EnumerateBranchRestrictionsAsync(new EnumerateBranchRestrictionsParameters(), token);

        /// <summary>
        /// Enumerate the information associated with a repository's branch restrictions asynchronously,
        /// doing requests page by page.
        /// </summary>
        /// <param name="parameters">Query parameters that can be used to filter the results.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<BranchRestriction> EnumerateBranchRestrictionsAsync(
            EnumerateBranchRestrictionsParameters parameters,
            CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<BranchRestriction>(
                _baseUrl + "branch-restrictions",
                parameters.ToDictionary(),
                parameters.PageLen,
                token);
        }
#endif

        /// <summary>
        /// Creates restrictions for the specified repository. You should specify a Content-Header with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PostBranchRestriction(BranchRestriction restriction)
        {
            return _sharpBucketV2.Post(restriction, _baseUrl + "branch-restrictions");
        }

        /// <summary>
        /// Creates restrictions for the specified repository. You should specify a Content-Header with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<BranchRestriction> PostBranchRestrictionAsync(BranchRestriction restriction, CancellationToken token = default)
        {
            return _sharpBucketV2.PostAsync(restriction, _baseUrl + "branch-restrictions", token);
        }

        /// <summary>
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(int restrictionId)
        {
            return _sharpBucketV2.Get<BranchRestriction>(_baseUrl + $"branch-restrictions/{restrictionId}");
        }

        /// <summary>
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<BranchRestriction> GetBranchRestrictionAsync(int restrictionId, CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync<BranchRestriction>(_baseUrl + $"branch-restrictions/{restrictionId}", token);
        }

        /// <summary>
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PutBranchRestriction(BranchRestriction restriction)
        {
            return _sharpBucketV2.Put(restriction, _baseUrl + $"branch-restrictions/{restriction.id}");
        }

        /// <summary>
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<BranchRestriction> PutBranchRestrictionAsync(BranchRestriction restriction, CancellationToken token = default)
        {
            return _sharpBucketV2.PutAsync(restriction, _baseUrl + $"branch-restrictions/{restriction.id}", token);
        }

        /// <summary>
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public void DeleteBranchRestriction(int restrictionId)
        {
            _sharpBucketV2.Delete(_baseUrl + $"branch-restrictions/{restrictionId}");
        }

        /// <summary>
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task DeleteBranchRestrictionAsync(int restrictionId, CancellationToken token = default)
        {
            return _sharpBucketV2.DeleteAsync(_baseUrl + $"branch-restrictions/{restrictionId}", token);
        }

        #endregion

        #region Diff Resource

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/diff+Resource
        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <returns></returns>
        public string GetDiff(string spec)
            => GetDiff(spec, new DiffParameters());

        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="parameters">Parameters for the diff.</param>
        /// <returns></returns>
        public string GetDiff(string spec, DiffParameters parameters)
        {
            return _sharpBucketV2.Get(_baseUrl + $"diff/{spec}", parameters.ToDictionary());
        }

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/diff+Resource
        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<string> GetDiffAsync(string spec, CancellationToken token = default)
            => GetDiffAsync(spec, new DiffParameters(), token);

        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="parameters">Parameters for the diff.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<string> GetDiffAsync(string spec, DiffParameters parameters, CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync(_baseUrl + $"diff/{spec}", parameters.ToDictionary(), token);
        }

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="spec">The patch spec.</param>
        /// <returns></returns>
        public string GetPatch(string spec)
        {
            return _sharpBucketV2.Get(_baseUrl + $"patch/{spec}");
        }

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="spec">The patch spec.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<string> GetPatchAsync(string spec, CancellationToken token = default)
        {
            return _sharpBucketV2.GetAsync(_baseUrl + $"patch/{spec}", token);
        }

        #endregion

        #region Commits resource

        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/commits#get
        /// <summary>
        /// Gets the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="max">Values greater than 0 will set a maximum number of records to return. 0 or less returns all.</param>
        public List<Commit> ListCommits(string branchOrTag = null, int max = 0)
            => ListCommits(branchOrTag, new ListCommitsParameters { Max = max });

        /// <summary>
        /// Gets the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public List<Commit> ListCommits(ListCommitsParameters commitsParameters)
            => ListCommits(null, commitsParameters);

        /// <summary>
        /// Gets the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public List<Commit> ListCommits(string branchOrTag, ListCommitsParameters commitsParameters)
        {
            var overrideUrl = _baseUrl + "commits/";
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return GetPaginatedValues<Commit>(overrideUrl, commitsParameters.Max, commitsParameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate the commits information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        public IEnumerable<Commit> EnumerateCommits(string branchOrTag = null)
            => EnumerateCommits(branchOrTag, new EnumerateCommitsParameters());

        /// <summary>
        /// Enumerate the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public IEnumerable<Commit> EnumerateCommits(EnumerateCommitsParameters commitsParameters)
             => EnumerateCommits(null, commitsParameters);

        /// <summary>
        /// Enumerate the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public IEnumerable<Commit> EnumerateCommits(string branchOrTag, EnumerateCommitsParameters commitsParameters)
        {
            var overrideUrl = _baseUrl + "commits/";
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return _sharpBucketV2.EnumeratePaginatedValues<Commit>(
                overrideUrl,
                commitsParameters.ToDictionary(),
                commitsParameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate the commits information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(CancellationToken token = default)
            => EnumerateCommitsAsync(null, new EnumerateCommitsParameters(), token);

        /// <summary>
        /// Enumerate the commits information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(string branchOrTag, CancellationToken token = default)
            => EnumerateCommitsAsync(branchOrTag, new EnumerateCommitsParameters(), token);

        /// <summary>
        /// Enumerate the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(EnumerateCommitsParameters commitsParameters, CancellationToken token = default)
            => EnumerateCommitsAsync(null, commitsParameters, token);

        /// <summary>
        /// Enumerate the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(string branchOrTag, EnumerateCommitsParameters commitsParameters, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + "commits/";
            if (!string.IsNullOrEmpty(branchOrTag))
            {
                overrideUrl += branchOrTag;
            }

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Commit>(overrideUrl, commitsParameters.ToDictionary(), commitsParameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public Commit GetCommit(string revision)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}";
            return _sharpBucketV2.Get<Commit>(overrideUrl);
        }

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<Commit> GetCommitAsync(string revision, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}";
            return _sharpBucketV2.GetAsync<Commit>(overrideUrl, token);
        }

        /// <summary>
        /// List of comments on the specified commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public List<Comment> ListCommitComments(string revision)
        {
            var overrideUrl = _baseUrl + $"commits/{revision}/comments/";
            return GetPaginatedValues<Comment>(overrideUrl);
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public Comment GetCommitComment(string revision, int commentId)
        {
            var overrideUrl = _baseUrl + $"commits/{revision}/comments/{revision}/{commentId}/";
            return _sharpBucketV2.Get<Comment>(overrideUrl);
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<Comment> GetCommitCommentAsync(string revision, int commentId, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commits/{revision}/comments/{revision}/{commentId}/";
            return _sharpBucketV2.GetAsync<Comment>(overrideUrl, token);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public UserRole ApproveCommit(string revision)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/approve/";
            return _sharpBucketV2.Post<UserRole>(null, overrideUrl);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<UserRole> ApproveCommitAsync(string revision, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/approve/";
            return _sharpBucketV2.PostAsync<UserRole>(null, overrideUrl, token);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public void DeleteCommitApproval(string revision)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/approve/";
            _sharpBucketV2.Delete(overrideUrl);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task DeleteCommitApprovalAsync(string revision, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/approve/";
            return _sharpBucketV2.DeleteAsync(overrideUrl, token);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        public BuildInfo AddNewBuildStatus(string revision, BuildInfo buildInfo)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/";
            return _sharpBucketV2.Post(buildInfo, overrideUrl);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<BuildInfo> AddNewBuildStatusAsync(string revision, BuildInfo buildInfo, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/";
            return _sharpBucketV2.PostAsync(buildInfo, overrideUrl, token);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <returns></returns>
        public BuildInfo GetBuildStatusInfo(string revision, string key)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/{key}";
            return _sharpBucketV2.Get<BuildInfo>(overrideUrl);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task<BuildInfo> GetBuildStatusInfoAsync(string revision, string key, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/{key}";
            return _sharpBucketV2.GetAsync<BuildInfo>(overrideUrl, token);
        }

        /// <summary>
        /// Used to update the current status of a build status object on the specific commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        /// /// <remarks>This operation can also be used to change other properties of the build status: state, name, description, url, refname. The key cannot be changed.</remarks>
        public BuildInfo ChangeBuildStatusInfo(string revision, string key, BuildInfo buildInfo)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/{key}";
            return _sharpBucketV2.Put(buildInfo, overrideUrl);
        }

        /// <summary>
        /// Used to update the current status of a build status object on the specific commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        /// /// <remarks>This operation can also be used to change other properties of the build status: state, name, description, url, refname. The key cannot be changed.</remarks>
        public Task<BuildInfo> ChangeBuildStatusInfoAsync(string revision, string key, BuildInfo buildInfo, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"commit/{revision}/statuses/build/{key}";
            return _sharpBucketV2.PutAsync(buildInfo, overrideUrl, token);
        }

        #endregion

        #region Default Reviewer Resource

        /// <summary>
        /// Adds a user as the default review for pull requests on a repository.
        /// </summary>
        /// <param name="targetUsername">The user to add as the default reviewer.</param>
        /// <returns></returns>
        public void PutDefaultReviewer(string targetUsername)
        {
            var overrideUrl = _baseUrl + $"default-reviewers/{targetUsername}";
            _sharpBucketV2.Put(new object(), overrideUrl);
        }

        /// <summary>
        /// Adds a user as the default review for pull requests on a repository.
        /// </summary>
        /// <param name="targetUsername">The user to add as the default reviewer.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public Task PutDefaultReviewerAsync(string targetUsername, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + $"default-reviewers/{targetUsername}";
            return _sharpBucketV2.PutAsync(new object(), overrideUrl, token: token);
        }

        #endregion

        #region Src resource

        public string GetMainBranchRevision()
        {
            var repoPath = $"{_accountName}/{_slug}";
            var rootSrcPath = $"src/";

            try
            {
                // calling the src endpoint redirect to the hash of the last commit of the main branch
                // https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
                var redirectLocation = this.GetSrcRootRedirectLocation(rootSrcPath);
                return redirectLocation.Segments[redirectLocation.Segments.Length - 1].TrimEnd('/');
            }
            catch (BitbucketV2Exception e) when (e.HttpStatusCode == HttpStatusCode.NotFound)
            {
                // mimic the error that bitbucket send when we perform calls on src endpoint with a revision name
                var errorResponse = new ErrorResponse { type = "Error", error = new Error { message = $"Repository {repoPath} not found" } };
                throw new BitbucketV2Exception(HttpStatusCode.NotFound, errorResponse);
            }
        }

        public async Task<string> GetMainBranchRevisionAsync(CancellationToken token = default)
        {
            var repoPath = $"{_accountName}/{_slug}";
            var rootSrcPath = $"src/";

            try
            {
                // calling the src endpoint redirect to the hash of the last commit of the main branch
                // https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
                var redirectLocation = await this.GetSrcRootRedirectLocationAsync(rootSrcPath, token);
                return redirectLocation.Segments[redirectLocation.Segments.Length - 1].TrimEnd('/');
            }
            catch (BitbucketV2Exception e) when (e.HttpStatusCode == HttpStatusCode.NotFound)
            {
                // mimic the error that bitbucket send when we perform calls on src endpoint with a revision name
                var errorResponse = new ErrorResponse { type = "Error", error = new Error { message = $"Repository {repoPath} not found" } };
                throw new BitbucketV2Exception(HttpStatusCode.NotFound, errorResponse);
            }
        }

        private Uri GetSrcRootRedirectLocation(string srcResourcePath)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath);
            return _sharpBucketV2.GetRedirectLocation(overrideUrl, new { format = "meta" });
        }

        private Task<Uri> GetSrcRootRedirectLocationAsync(string srcResourcePath, CancellationToken token)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(_baseUrl, srcResourcePath);
            return _sharpBucketV2.GetRedirectLocationAsync(overrideUrl, new { format = "meta" }, token);
        }

        /// <summary>
        /// Get a Src resource that allows to browse the content of the repository
        /// </summary>
        /// <remarks>
        /// If revision is null a non async request will occurs.
        /// if you want a fullly async experience, you should do yourseulf an explicit call to <see cref="GetMainBranchRevisionAsync(CancellationToken)"/>
        /// and then provide the result in the <paramref name="revision"/> parameter.
        /// </remarks>
        /// <param name="revision">The name of the revision to browse. This may be a commit hash, a branch name, a tag name, or null to target the last commit of the main branch.</param>
        /// <param name="path">An optional path to a sub directory if you want to start to browse somewhere else that at the root path.</param>
        public SrcResource SrcResource(string revision = null, string path = null)
        {
            return new SrcResource(_repositoriesEndPoint, _accountName, _slug, revision, path);
        }

        #endregion

        #region tags resource

        private TagsResource _tagsResource;

        /// <summary>
        /// Gets the reqource that allow to manage tags for this repository.
        /// </summary>
        public TagsResource TagsResource => this._tagsResource ??
                                                (_tagsResource = new TagsResource(_accountName, _slug, _repositoriesEndPoint));

        #endregion
    }
}