﻿using System;
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
            return _repositoriesEndPoint.GetRepository(_accountName, _slug);
        }

        /// <summary>
        /// Returns a single repository.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<Repository> GetRepositoryAsync(CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetRepositoryAsync(_accountName, _slug, token);
        }

        /// <summary>
        /// Removes a repository.  
        /// </summary>
        /// <returns></returns>
        public void DeleteRepository()
        {
            _repositoriesEndPoint.DeleteRepository(_accountName, _slug);
        }

        /// <summary>
        /// Removes a repository.  
        /// </summary>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task DeleteRepositoryAsync(CancellationToken token = default)
        {
            await _repositoriesEndPoint.DeleteRepositoryAsync(_accountName, _slug, token);
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repository">The repository to create.</param>
        /// <returns>The created repository.</returns>
        public Repository PostRepository(Repository repository)
        {
            return _repositoriesEndPoint.PostRepository(repository, _accountName);
        }

        /// <summary>
        /// Creates a new repository.
        /// </summary>
        /// <param name="repository">The repository to create.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The created repository.</returns>
        public async Task<Repository> PostRepositoryAsync(Repository repository, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PostRepositoryAsync(repository, _accountName, token);
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
            return _repositoriesEndPoint.ListWatchers(_accountName, _slug, max);
        }

        /// <summary>
        /// Enumerate accounts watching a repository.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        public IEnumerable<UserShort> EnumerateWatchers(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumerateWatchers(_accountName, _slug, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate accounts watching a repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<UserShort> EnumerateWatchersAsync(int? pageLen = null, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumerateWatchersAsync(_accountName, _slug, pageLen, token);
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
            return _repositoriesEndPoint.ListForks(_accountName, _slug, max);
        }

        /// <summary>
        /// Enumerate repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        public IEnumerable<Repository> EnumerateForks(int? pageLen = null)
        {
            return _repositoriesEndPoint.EnumerateForks(_accountName, _slug, pageLen);
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
            return _repositoriesEndPoint.EnumerateForksAsync(_accountName, _slug, pageLen, token);
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
            return _repositoriesEndPoint.ListBranchRestrictions(_accountName, _slug, parameters, parameters.Max);
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
            return _repositoriesEndPoint.EnumerateBranchRestrictions(
                _accountName,
                _slug,
                parameters,
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
            return _repositoriesEndPoint.EnumerateBranchRestrictionsAsync(
                _accountName,
                _slug,
                parameters,
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
            return _repositoriesEndPoint.PostBranchRestriction(_accountName, _slug, restriction);
        }

        /// <summary>
        /// Creates restrictions for the specified repository. You should specify a Content-Header with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<BranchRestriction> PostBranchRestrictionAsync(BranchRestriction restriction, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PostBranchRestrictionAsync(_accountName, _slug, restriction, token);
        }

        /// <summary>
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(int restrictionId)
        {
            return _repositoriesEndPoint.GetBranchRestriction(_accountName, _slug, restrictionId);
        }

        /// <summary>
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<BranchRestriction> GetBranchRestrictionAsync(int restrictionId, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetBranchRestrictionAsync(_accountName, _slug, restrictionId, token);
        }

        /// <summary>
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PutBranchRestriction(BranchRestriction restriction)
        {
            return _repositoriesEndPoint.PutBranchRestriction(_accountName, _slug, restriction);
        }

        /// <summary>
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<BranchRestriction> PutBranchRestrictionAsync(BranchRestriction restriction, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.PutBranchRestrictionAsync(_accountName, _slug, restriction, token);
        }

        /// <summary>
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public void DeleteBranchRestriction(int restrictionId)
        {
            _repositoriesEndPoint.DeleteBranchRestriction(_accountName, _slug, restrictionId);
        }

        /// <summary>
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task DeleteBranchRestrictionAsync(int restrictionId, CancellationToken token = default)
        {
            await _repositoriesEndPoint.DeleteBranchRestrictionAsync(_accountName, _slug, restrictionId, token);
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
        public string GetDiff(string spec) => GetDiff(spec, new DiffParameters());

        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="parameters">Parameters for the diff.</param>
        /// <returns></returns>
        public string GetDiff(string spec, DiffParameters parameters)
        {
            return _repositoriesEndPoint.GetDiff(_accountName, _slug, spec, parameters);
        }

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/diff+Resource
        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<string> GetDiffAsync(string spec, CancellationToken token = default) => await GetDiffAsync(spec, new DiffParameters(), token);

        /// <summary>
        /// Gets the diff for the current repository.
        /// </summary>
        /// <param name="spec">The diff spec (e.g., de3f2..78ab1).</param>
        /// <param name="parameters">Parameters for the diff.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<string> GetDiffAsync(string spec, DiffParameters parameters, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetDiffAsync(_accountName, _slug, spec, parameters, token);
        }

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="spec">The patch spec.</param>
        /// <returns></returns>
        public string GetPatch(string spec)
        {
            return _repositoriesEndPoint.GetPatch(_accountName, _slug, spec);
        }

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="spec">The patch spec.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<string> GetPatchAsync(string spec, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetPatchAsync(_accountName, _slug, spec, token);
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
        {
            return _repositoriesEndPoint.ListCommits(_accountName, _slug, branchOrTag, new ListCommitsParameters { Max = max });
        }

        /// <summary>
        /// Gets the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public List<Commit> ListCommits(ListCommitsParameters commitsParameters)
        {
            return _repositoriesEndPoint.ListCommits(_accountName, _slug, null, commitsParameters);
        }

        /// <summary>
        /// Gets the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public List<Commit> ListCommits(string branchOrTag, ListCommitsParameters commitsParameters)
        {
            return _repositoriesEndPoint.ListCommits(_accountName, _slug, branchOrTag, commitsParameters);
        }

        /// <summary>
        /// Enumerate the commits information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        public IEnumerable<Commit> EnumerateCommits(string branchOrTag = null)
        {
            return _repositoriesEndPoint.EnumerateCommits(_accountName, _slug, branchOrTag, new EnumerateCommitsParameters());
        }

        /// <summary>
        /// Enumerate the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public IEnumerable<Commit> EnumerateCommits(EnumerateCommitsParameters commitsParameters)
        {
            return _repositoriesEndPoint.EnumerateCommits(_accountName, _slug, null, commitsParameters);
        }

        /// <summary>
        /// Enumerate the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public IEnumerable<Commit> EnumerateCommits(string branchOrTag, EnumerateCommitsParameters commitsParameters)
        {
            return _repositoriesEndPoint.EnumerateCommits(_accountName, _slug, branchOrTag, commitsParameters);
        }

#if CS_8
        /// <summary>
        /// Enumerate the commits information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumerateCommitsAsync(_accountName, _slug, null, new EnumerateCommitsParameters(), token);
        }

        /// <summary>
        /// Enumerate the commits information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(string branchOrTag, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumerateCommitsAsync(_accountName, _slug, branchOrTag, new EnumerateCommitsParameters(), token);
        }

        /// <summary>
        /// Enumerate the commit information associated with a repository.
        /// By default, this call returns all the commits across all branches, bookmarks, and tags.
        /// The newest commit is first.
        /// </summary>
        /// <param name="commitsParameters">Parameters that allow to filter the commits to return.</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(EnumerateCommitsParameters commitsParameters, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumerateCommitsAsync(_accountName, _slug, null, commitsParameters, token);
        }

        /// <summary>
        /// Enumerate the commit information associated with a repository in a specified branch.
        /// The newest commit is first.
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="commitsParameters">Optional parameters that allow to filter the commits to return.</param>
        public IAsyncEnumerable<Commit> EnumerateCommitsAsync(string branchOrTag, EnumerateCommitsParameters commitsParameters, CancellationToken token = default)
        {
            return _repositoriesEndPoint.EnumerateCommitsAsync(_accountName, _slug, branchOrTag, commitsParameters, token);
        }
#endif

        public CommitResource Commit(string revision)
        {
            return new CommitResource(this, revision);
        }

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public Commit GetCommit(string revision)
        {
            return _repositoriesEndPoint.GetCommit(_accountName, _slug, revision);
        }

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<Commit> GetCommitAsync(string revision, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetCommitAsync(_accountName, _slug, revision, token);
        }

        /// <summary>
        /// List of comments on the specified commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        [Obsolete("Prefer Commit(revision).Comments.List() or any other listing method in Commit(revision).Comments")]
        public List<Comment> ListCommitComments(string revision)
        {
            return Commit(revision).Comments.List();
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="commentId">The comment identifier.</param>
        [Obsolete("Prefer Commit(revision).Comments.Comment(commentId).Get()")]
        public Comment GetCommitComment(string revision, int commentId)
        {
            return Commit(revision).Comments.Comment(commentId).Get();
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="token">The cancellation token</param>
        [Obsolete("Prefer Commit(revision).Comments.Comment(commentId).GetAsync(token)")]
        public Task<Comment> GetCommitCommentAsync(string revision, int commentId, CancellationToken token = default)
        {
            return Commit(revision).Comments.Comment(commentId).GetAsync(token);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public UserRole ApproveCommit(string revision)
        {
            return _repositoriesEndPoint.ApproveCommit(_accountName, _slug, revision);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<UserRole> ApproveCommitAsync(string revision, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.ApproveCommitAsync(_accountName, _slug, revision, token);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public void DeleteCommitApproval(string revision)
        {
            _repositoriesEndPoint.DeleteCommitApproval(_accountName, _slug, revision);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task DeleteCommitApprovalAsync(string revision, CancellationToken token = default)
        {
            await _repositoriesEndPoint.DeleteCommitApprovalAsync(_accountName, _slug, revision, token);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        public BuildInfo AddNewBuildStatus(string revision, BuildInfo buildInfo)
        {
            return _repositoriesEndPoint.AddNewBuildStatus(_accountName, _slug, revision, buildInfo);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<BuildInfo> AddNewBuildStatusAsync(string revision, BuildInfo buildInfo, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.AddNewBuildStatusAsync(_accountName, _slug, revision, buildInfo, token);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <returns></returns>
        public BuildInfo GetBuildStatusInfo(string revision, string key)
        {
            return _repositoriesEndPoint.GetBuildStatusInfo(_accountName, _slug, revision, key);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task<BuildInfo> GetBuildStatusInfoAsync(string revision, string key, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.GetBuildStatusInfoAsync(_accountName, _slug, revision, key, token);
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
            return _repositoriesEndPoint.ChangeBuildStatusInfo(_accountName, _slug, revision, key, buildInfo);
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
        public async Task<BuildInfo> ChangeBuildStatusInfoAsync(string revision, string key, BuildInfo buildInfo, CancellationToken token = default)
        {
            return await _repositoriesEndPoint.ChangeBuildStatusInfoAsync(_accountName, _slug, revision, key, buildInfo, token);
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
            _repositoriesEndPoint.PutDefaultReviewer(_accountName, _slug, targetUsername);
        }

        /// <summary>
        /// Adds a user as the default review for pull requests on a repository.
        /// </summary>
        /// <param name="targetUsername">The user to add as the default reviewer.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns></returns>
        public async Task PutDefaultReviewerAsync(string targetUsername, CancellationToken token = default)
        {
            await _repositoriesEndPoint.PutDefaultReviewerAsync(_accountName, _slug, targetUsername, token);
        }

        #endregion

        #region Src resource

        public string GetMainBranchRevision()
        {
            var repoPath = $"{_accountName}/{_slug}";
            var rootSrcPath = $"{repoPath}/src/";

            try
            {
                // calling the src endpoint redirect to the hash of the last commit of the main branch
                // https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
                var redirectLocation = _repositoriesEndPoint.GetSrcRootRedirectLocation(rootSrcPath);
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
            var rootSrcPath = $"{repoPath}/src/";

            try
            {
                // calling the src endpoint redirect to the hash of the last commit of the main branch
                // https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
                var redirectLocation = await _repositoriesEndPoint.GetSrcRootRedirectLocationAsync(rootSrcPath, token);
                return redirectLocation.Segments[redirectLocation.Segments.Length - 1].TrimEnd('/');
            }
            catch (BitbucketV2Exception e) when (e.HttpStatusCode == HttpStatusCode.NotFound)
            {
                // mimic the error that bitbucket send when we perform calls on src endpoint with a revision name
                var errorResponse = new ErrorResponse { type = "Error", error = new Error { message = $"Repository {repoPath} not found" } };
                throw new BitbucketV2Exception(HttpStatusCode.NotFound, errorResponse);
            }
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