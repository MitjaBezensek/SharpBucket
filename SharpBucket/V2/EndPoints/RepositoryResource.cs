using System.Collections.Generic;
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
    public class RepositoryResource
    {
        private readonly RepositoriesEndPoint _repositoriesEndPoint;
        private readonly string _accountName;
        private readonly string _slug;

        #region Repository Resource

        public RepositoryResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
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
        /// Removes a repository.  
        /// </summary>
        /// <returns></returns>
        public Repository DeleteRepository()
        {
            return _repositoriesEndPoint.DeleteRepository(_accountName, _slug);
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
        /// Gets the list of accounts watching a repository. 
        /// </summary>
        /// <returns></returns>
        public List<Watcher> ListWatchers()
        {
            return _repositoriesEndPoint.ListWatchers(_accountName, _slug);
        }

        /// <summary>
        /// List of repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <returns></returns>
        public List<Fork> ListForks()
        {
            return _repositoriesEndPoint.ListForks(_accountName, _slug);
        }

        #endregion

        #region BranchResource

        private BranchResource _branchesResource;

        public BranchResource BranchesResource => this._branchesResource ??
                                                (_branchesResource = new BranchResource(_accountName, _slug, _repositoriesEndPoint));

        /// <summary>
        /// Removes a branch.  
        /// </summary>
        /// <returns></returns>
        public Branch DeleteBranch(string branchName)
        {
            return _repositoriesEndPoint.DeleteBranch(_accountName, _slug, branchName);
        }

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
        /// https://confluence.atlassian.com/display/BITBUCKET/branch-restrictions+Resource
        /// <summary>
        /// List the information associated with a repository's branch restrictions. 
        /// </summary>
        /// <returns></returns>
        public List<BranchRestriction> ListBranchRestrictions()
        {
            return _repositoriesEndPoint.ListBranchRestrictions(_accountName, _slug);
        }

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
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(int restrictionId)
        {
            return _repositoriesEndPoint.GetBranchRestriction(_accountName, _slug, restrictionId);
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
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction DeleteBranchRestriction(int restrictionId)
        {
            return _repositoriesEndPoint.DeleteBranchRestriction(_accountName, _slug, restrictionId);
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

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="spec">The patch spec.</param>
        /// <returns></returns>
        public string GetPatch(string spec)
        {
            return _repositoriesEndPoint.GetPatch(_accountName, _slug, spec);
        }

        #endregion

        #region Commits resource

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/commits+or+commit+Resource
        /// <summary>
        /// Gets the commit information associated with a repository. 
        /// By default, this call returns all the commits across all branches, bookmarks, and tags. The newest commit is first. 
        /// </summary>
        /// <param name="branchOrTag">The branch or tag to get, for example, master or default.</param>
        /// <param name="max">Values greater than 0 will set a maximum number of records to return. 0 or less returns all.</param>
        /// <returns></returns>
        public List<Commit> ListCommits(string branchOrTag = null, int max = 0)
        {
            return _repositoriesEndPoint.ListCommits(_accountName, _slug, branchOrTag, max);
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
        /// List of comments on the specified commit.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public List<Comment> ListCommitComments(string revision)
        {
            return _repositoriesEndPoint.ListCommitComments(_accountName, _slug, revision);
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public Comment GetCommitComment(string revision, int commentId)
        {
            return _repositoriesEndPoint.GetCommitComment(_accountName, _slug, revision, commentId);
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
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The SHA1 of the commit.</param>
        /// <returns></returns>
        public void DeleteCommitApproval(string revision)
        {
            _repositoriesEndPoint.DeleteCommitApproval(_accountName, _slug, revision);
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

        #endregion

        #region Src resource

        /// <summary>
        /// Get a Src resource that allows to browse the content of the repository
        /// </summary>
        /// <param name="revision">The name of the revision to browse. This may be a commit hash, a branch name, a tag name, or null to target the last commit of the main branch.</param>
        /// <param name="path">An optional path to a sub directory if you want to start to browse somewhere else that at the root path.</param>
        public SrcResource SrcResource(string revision = null, string path = null)
        {
            return new SrcResource(_repositoriesEndPoint, _accountName, _slug, revision, path);
        }

        #endregion
    }
}