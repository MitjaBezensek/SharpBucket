using System.Collections.Generic;
using System.Net;
using Serilog;
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
        private readonly string _repository;

        #region Repository Resource

        public RepositoryResource(string accountName, string repository, RepositoriesEndPoint repositoriesEndPoint)
        {
            _repository = repository;
            _accountName = accountName;
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Returns a single repository.
        /// </summary>
        /// <returns></returns>
        public Repository GetRepository()
        {
            return _repositoriesEndPoint.GetRepository(_accountName, _repository);
        }

        /// <summary>
        /// With Logging.
        /// Returns a single repository.
        /// </summary>
        /// <returns></returns>
        public Repository GetRepository(ILogger logger)
        {
            return _repositoriesEndPoint.GetRepository(logger, _accountName, _repository);
        }

        /// <summary>
        /// Removes a repository.  
        /// </summary>
        /// <returns></returns>
        public HttpStatusCode DeleteRepository()
        {
            return _repositoriesEndPoint.DeleteRepository(_accountName, _repository);
        }


        /// <summary>
        /// With Logging.
        /// Removes a repository.  
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        public HttpStatusCode DeleteRepository(ILogger logger)
        {
            return _repositoriesEndPoint.DeleteRepository(logger, _accountName, _repository);
        }

        public Repository PostRepository(Repository repository)
        {
            return _repositoriesEndPoint.PostRepository(repository, _accountName);
        }
        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public Repository PostRepository(ILogger logger, Repository repository)
        {
            return _repositoriesEndPoint.PostRepository(logger, repository, _accountName);
        }


        /// <summary>
        /// Put a specific repository
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public Repository PutRepository(Repository repository)
        {
            return _repositoriesEndPoint.PutRepository(repository, _accountName, repository.name.ToLowerInvariant());
        }

        /// <summary>
        /// Put a specific repository
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public Repository PutRepository(ILogger logger, Repository repository)
        {
            return _repositoriesEndPoint.PutRepository(logger, repository, _accountName, repository.name.ToLowerInvariant());
        }

        /// <summary>
        /// Gets the list of accounts watching a repository. 
        /// </summary>
        /// <returns></returns>
        public List<Watcher> ListWatchers()
        {
            return _repositoriesEndPoint.ListWatchers(_accountName, _repository);
        }

        /// <summary>
        /// Gets the list of accounts watching a repository. 
        /// </summary>
        /// <returns></returns>
        public List<Watcher> ListWatchers(ILogger logger)
        {
            return _repositoriesEndPoint.ListWatchers(logger, _accountName, _repository);
        }

        /// <summary>
        /// List of repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <returns></returns>
        public List<Fork> ListForks()
        {
            return _repositoriesEndPoint.ListForks(_accountName, _repository);
        }

        /// <summary>
        /// List of repository forks, This call returns a repository object for each fork.
        /// </summary>
        /// <returns></returns>
        public List<Fork> ListForks(ILogger logger)
        {
            return _repositoriesEndPoint.ListForks(logger, _accountName, _repository);
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
            return new PullRequestsResource(_accountName, _repository, _repositoriesEndPoint);
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
            return _repositoriesEndPoint.ListBranchRestrictions(_accountName, _repository);
        }

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/branch-restrictions+Resource
        /// <summary>
        /// With Logging.
        /// List the information associated with a repository's branch restrictions. 
        /// </summary>
        /// <returns></returns>
        public List<BranchRestriction> ListBranchRestrictions(ILogger logger)
        {
            return _repositoriesEndPoint.ListBranchRestrictions(logger, _accountName, _repository);
        }

        /// <summary>
        /// Creates restrictions for the specified repository. You should specify a Content-Header with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PostBranchRestriction(BranchRestriction restriction)
        {
            return _repositoriesEndPoint.PostBranchRestriction(_accountName, _repository, restriction);
        }

        /// <summary>
        /// With Logging.
        /// Creates restrictions for the specified repository. You should specify a Content-Header with this call. 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PostBranchRestriction(ILogger logger, BranchRestriction restriction)
        {
            return _repositoriesEndPoint.PostBranchRestriction(logger, _accountName, _repository, restriction);
        }

        /// <summary>
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(int restrictionId)
        {
            return _repositoriesEndPoint.GetBranchRestriction(_accountName, _repository, restrictionId);
        }

        /// <summary>
        /// With Logging.
        /// Gets the information associated with specific restriction. 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(ILogger logger, int restrictionId)
        {
            return _repositoriesEndPoint.GetBranchRestriction(logger, _accountName, _repository, restrictionId);
        }

        /// <summary>
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PutBranchRestriction(BranchRestriction restriction)
        {
            return _repositoriesEndPoint.PutBranchRestriction(_accountName, _repository, restriction);
        }

        /// <summary>
        /// With Logging
        /// Updates a specific branch restriction. You cannot change the kind value with this call. 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="restriction">The branch restriction.</param>
        /// <returns></returns>
        public BranchRestriction PutBranchRestriction(ILogger logger, BranchRestriction restriction)
        {
            return _repositoriesEndPoint.PutBranchRestriction(logger, _accountName, _repository, restriction);
        }

        /// <summary>
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public HttpStatusCode DeleteBranchRestriction(int restrictionId)
        {
            return _repositoriesEndPoint.DeleteBranchRestriction(_accountName, _repository, restrictionId);
        }
        /// <summary>
        /// With Logging
        /// Deletes the specified restriction.  
        /// </summary>
        /// <param name="restrictionId">The restriction's identifier.</param>
        /// <returns></returns>
        public HttpStatusCode DeleteBranchRestriction(ILogger logger, int restrictionId)
        {
            return _repositoriesEndPoint.DeleteBranchRestriction(logger, _accountName, _repository, restrictionId);
        }

        #endregion

        #region Diff Resource

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/diff+Resource
        /// <summary>
        /// Gets the diff for the current repository.  
        /// </summary>
        /// <param name="options">The diff options.</param>
        /// <returns></returns>
        public object GetDiff(object options)
        {
            return _repositoriesEndPoint.GetDiff(_accountName, _repository, options);
        }

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/diff+Resource
        /// <summary>
        /// With Logging
        /// Gets the diff for the current repository.  
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options">The diff options.</param>
        /// <returns></returns>
        public object GetDiff(ILogger logger, object options)
        {
            return _repositoriesEndPoint.GetDiff(logger, _accountName, _repository, options);
        }

        /// <summary>
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="options">The patch options.</param>
        /// <returns></returns>
        public object GetPatch(object options)
        {
            return _repositoriesEndPoint.GetPatch(_accountName, _repository, options);
        }

        /// <summary>
        /// With Logging
        /// Gets the patch for an individual specification. 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options">The patch options.</param>
        /// <returns></returns>
        public object GetPatch(ILogger logger, object options)
        {
            return _repositoriesEndPoint.GetPatch(logger, _accountName, _repository, options);
        }

        #endregion

        #region Commits resource

        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/commits+or+commit+Resource
        /// <summary>
        /// Gets the commit information associated with a repository. 
        /// By default, this call returns all the commits across all branches, bookmarks, and tags. The newest commit is first. 
        /// </summary>
        /// <param name="branchortag">The branch or tag to get, for example, master or default.</param>
        /// <param name="max">Values greater than 0 will set a maximum number of records to return. 0 or less returns all.</param>
        /// <returns></returns>
        public List<Commit> ListCommits(string branchortag = null, int max = 0)
        {
            return _repositoriesEndPoint.ListCommits(_accountName, _repository, branchortag, max);
        }
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/commits+or+commit+Resource
        /// <summary>
        /// Gets the commit information associated with a repository. 
        /// By default, this call returns all the commits across all branches, bookmarks, and tags. The newest commit is first. 
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="branchortag">The branch or tag to get, for example, master or default.</param>
        /// <param name="max">Values greater than 0 will set a maximum number of records to return. 0 or less returns all.</param>
        /// <returns></returns>
        public List<Commit> ListCommits(ILogger logger, string branchortag = null, int max = 0)
        {
            return _repositoriesEndPoint.ListCommits(logger, _accountName, _repository, branchortag, max);
        }

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// </summary>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public Commit GetCommit(string revision)
        {
            return _repositoriesEndPoint.GetCommit(_accountName, _repository, revision);
        }

        /// <summary>
        /// Gets the information associated with an individual commit. 
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public Commit GetCommit(ILogger logger, string revision)
        {
            return _repositoriesEndPoint.GetCommit(logger, _accountName, _repository, revision);
        }

        /// <summary>
        /// List of comments on the specified commit.
        /// </summary>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public List<Comment> ListCommitComments(string revision)
        {
            return _repositoriesEndPoint.ListCommitComments(_accountName, _repository, revision);
        }

        /// <summary>
        /// List of comments on the specified commit.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public List<Comment> ListCommitComments(ILogger logger, string revision)
        {
            return _repositoriesEndPoint.ListCommitComments(logger, _accountName, _repository, revision);
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// </summary>
        /// <param name="revision">The commit's SHA1.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public object GetCommitComment(string revision, int commentId)
        {
            return _repositoriesEndPoint.GetCommitComment(_accountName, _repository, revision, commentId);
        }

        /// <summary>
        /// To get an individual commit comment, just follow the object's self link.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <returns></returns>
        public object GetCommitComment(ILogger logger, string revision, int commentId)
        {
            return _repositoriesEndPoint.GetCommitComment(logger, _accountName, _repository, revision, commentId);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// </summary>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public object ApproveCommit(string revision)
        {
            return _repositoriesEndPoint.ApproveCommit(_accountName, _repository, revision);
        }

        /// <summary>
        /// Give your approval on a commit.  
        /// You can only approve a comment on behalf of the authenticated account.  This returns the participant object for the current user.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public object ApproveCommit(ILogger logger, string revision)
        {
            return _repositoriesEndPoint.ApproveCommit(logger, _accountName, _repository, revision);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// </summary>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public object DeleteCommitApproval(string revision)
        {
            return _repositoriesEndPoint.DeleteCommitApproval(_accountName, _repository, revision);
        }

        /// <summary>
        /// Revoke your approval of a commit. You can remove approvals on behalf of the authenticated account. 
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1.</param>
        /// <returns></returns>
        public object DeleteCommitApproval(ILogger logger, string revision)
        {
            return _repositoriesEndPoint.DeleteCommitApproval(logger, _accountName, _repository, revision);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// </summary>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        public object AddNewBuildStatus(string revision, BuildInfo buildInfo)
        {
            return _repositoriesEndPoint.AddNewBuildStatus(_accountName, _repository, revision, buildInfo);
        }

        /// <summary>
        /// Creates a new build status against the specified commit. If the specified key already exists, the existing status object will be overwritten.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        public object AddNewBuildStatus(ILogger logger, string revision, BuildInfo buildInfo)
        {
            return _repositoriesEndPoint.AddNewBuildStatus(logger, _accountName, _repository, revision, buildInfo);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// </summary>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="key">The build status' unique key</param>
        /// <returns></returns>
        public BuildInfo GetBuildStatusInfo(string revision, string key)
        {
            return _repositoriesEndPoint.GetBuildStatusInfo(_accountName, _repository, revision, key);
        }

        /// <summary>
        /// Returns the specified build status for a commit.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="key">The build status' unique key</param>
        /// <returns></returns>
        public BuildInfo GetBuildStatusInfo(ILogger logger, string revision, string key)
        {
            return _repositoriesEndPoint.GetBuildStatusInfo(logger, _accountName, _repository, revision, key);
        }

        /// <summary>
        /// Used to update the current status of a build status object on the specific commit.
        /// </summary>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        /// /// <remarks>This operation can also be used to change other properties of the build status: state, name, description, url, refname. The key cannot be changed.</remarks>
        public object ChangeBuildStatusInfo(string revision, string key, BuildInfo buildInfo)
        {
            return _repositoriesEndPoint.ChangeBuildStatusInfo(_accountName, _repository, revision, key, buildInfo);
        }

        /// <summary>
        /// Used to update the current status of a build status object on the specific commit.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="revision">The commit's SHA1</param>
        /// <param name="key">The build status' unique key</param>
        /// <param name="buildInfo">The new commit status object</param>
        /// <returns></returns>
        /// /// <remarks>This operation can also be used to change other properties of the build status: state, name, description, url, refname. The key cannot be changed.</remarks>
        public object ChangeBuildStatusInfo(ILogger logger, string revision, string key, BuildInfo buildInfo)
        {
            return _repositoriesEndPoint.ChangeBuildStatusInfo(logger, _accountName, _repository, revision, key, buildInfo);
        }

        #endregion

        #region Default Reviewer Resource

        /// <summary>
        /// Adds a user as the default review for pull requests on a repository.
        /// </summary>
        /// <param name="targetUsername">The user to add as the default reviewer.</param>
        /// <returns></returns>
        public object PutDefaultReviewer(string targetUsername)
        {
            return _repositoriesEndPoint.PutDefaultReviewer(_accountName, _repository, targetUsername);
        }

        /// <summary>
        /// Adds a user as the default review for pull requests on a repository.
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="targetUsername">The user to add as the default reviewer.</param>
        /// <returns></returns>
        public object PutDefaultReviewer(ILogger logger, string targetUsername)
        {
            return _repositoriesEndPoint.PutDefaultReviewer(logger, _accountName, _repository, targetUsername);
        }

        #endregion


    }
}