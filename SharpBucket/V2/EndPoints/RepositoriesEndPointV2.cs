using System;
using System.Collections.Generic;
using SharpBucket.V2.Pocos;
using Comment = SharpBucket.V2.Pocos.Comment;
using Repository = SharpBucket.V2.Pocos.Repository;

namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// The repositories endpoint has a number of resources you can use to manage repository resources. 
    /// For all repository resources, you supply a  repo_slug that identifies the specific repository.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint
    /// </summary>
    public class RepositoriesEndPointV2{
        private readonly SharpBucketV2 _sharpBucketV2;
        private readonly string _baseUrl;

        public RepositoriesEndPointV2(SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "repositories/";
        }

        /// <summary>
        /// Get the PullRequests resource.
        /// </summary>
        /// <param name="accountName">The account whose pull requst resource you wish to get.</param>
        /// <param name="repository">The repository whose pull request resource you wish to get.</param>
        /// <returns></returns>
        public PullRequestsResourceV2 PullReqests(string accountName, string repository){
            return new PullRequestsResourceV2(accountName, repository, this);
        }

        /// <summary>
        /// Get the Repository resource.
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        public RepositoryResourceV2 Repository(string accountName, string repository){
            return new RepositoryResourceV2(accountName, repository, this);
        }

        /// <summary>
        /// List all the repositories for the selected account.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(string accountName){
            var overrideUrl = _baseUrl + accountName + "/";
            return _sharpBucketV2.Get(new RepositoryInfo(), overrideUrl).values;
        }

        /// <summary>
        /// List all the public repositories.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(){
            return _sharpBucketV2.Get(new RepositoryInfo(), _baseUrl).values;
        }

        /// <summary>
        /// Get the information for a specific repository.
        /// </summary>
        /// <param name="accountName">The account of the repository.</param>
        /// <param name="repository">The repository that you wish to get.</param>
        /// <returns></returns>
        public Repository GetRepository(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Get(new Repository(), overrideUrl);
        }

        /// <summary>
        /// Add a new repository
        /// </summary>
        /// <param name="repo">The repository that you wish to add.</param>
        /// <param name="accountName">The account that will be the owner of the repository.</param>
        /// <param name="repository">The repository name.</param>
        /// <returns></returns>
        public Repository PutRepository(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        /// <summary>
        /// Delete a repository.
        /// </summary>
        /// <param name="repo">The repository that you wish to delete.</param>
        /// <param name="accountName">The account that is the owner of the repository.</param>
        /// <param name="repository">The repository name that you wish to delete.</param>
        /// <returns></returns>
        public Repository DeleteRepository(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(repo, overrideUrl);
        }

        private string GetRepositoryUrl(string accountName, string repository, string append){
            var format = _baseUrl + "{0}/{1}/{2}";
            return string.Format(format, accountName, repository, append);
        }

        /// <summary>
        /// List all the watchers of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose watchers you wish to get.</param>
        /// <returns></returns>
        public List<Watcher> ListWatchers(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
            return _sharpBucketV2.Get(new WatcherInfo(), overrideUrl).values;
        }

        /// <summary>
        /// List all the forks of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose forks you wish to get.</param>
        /// <returns></returns>
        public List<Fork> ListForks(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return _sharpBucketV2.Get(new ForkInfo(), overrideUrl).values;
        }

        /// <summary>
        /// List all the branches of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose branches you wish to get.</param>
        /// <returns></returns>
        public List<BranchRestriction> ListBranchRestrictions(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Get(new BranchRestrictionInfo(), overrideUrl).values;
        }

        /// <summary>
        /// Add a branch restriction.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository to which you wish to add a restriction.</param>
        /// <param name="restriction">The branch restriction that you wish to add.</param>
        /// <returns></returns>
        public BranchRestriction PostBranchRestriction(string accountName, string repository, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        /// <summary>
        /// Get a selected branch restriction.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose branch restriction you wish to get.</param>
        /// <param name="restrictionId">The Id of the branch restriction that you wish to get.</param>
        /// <returns></returns>
        public BranchRestriction GetBranchRestriction(string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(new BranchRestriction(), overrideUrl);
        }

        /// <summary>
        /// Update a branch restriction.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose branch restriction you wish to update.</param>
        /// <param name="restrictionId">The Id of the restriction that you wish to update.</param>
        /// <param name="restriction">The updated restriction.</param>
        /// <returns></returns>
        public BranchRestriction PutBranchRestriction(string accountName, string repository, int restrictionId, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        /// <summary>
        /// Delete a branch restriction.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose branch restriction you wish to delete.</param>
        /// <param name="restrictionId">The Id of the restriction that you wish to delete.</param>
        /// <returns></returns>
        public BranchRestriction DeleteBranchRestriction(string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(new BranchRestriction(), overrideUrl);
        }

        /// <summary>
        /// Gets the diff for the current repository
        /// </summary>
        /// <param name="repo">The repository whose diff you wish to get.</param>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The name of the repository.</param>
        /// <param name="options">The diff options.</param>
        /// <returns></returns>
        public object GetDiff(Repository repo, string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        /// <summary>
        /// Get the patch for the repository.
        /// </summary>
        /// <param name="repo">The repository whose patch you wish to get.</param>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The name of the repository whose patch you wish to get.</param>
        /// <param name="options">The patch options.</param>
        /// <returns></returns>
        public object GetPatch(Repository repo, string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        /// <summary>
        /// Get all the commits for the repository.
        /// </summary>
        /// <param name="repo">The repository whose commits you wish to get.</param>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository name whose commits you wish to get.</param>
        /// <returns></returns>
        public object ListCommits(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        /// <summary>
        /// Get a selected commit for the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository name whose commit you wish to get.</param>
        /// <param name="commitId">The Id of the commit that you wish to get.</param>
        /// <returns></returns>
        public object GetCommit(string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// List all the comments for a selected commit of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose commit comments you wish to get.</param>
        /// <param name="commitId">The Id of the commit whose comments you wish to get.</param>
        /// <returns></returns>
        public List<object> ListCommitComments(string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/comments/");
            return _sharpBucketV2.Get(new List<object>(), overrideUrl);
        }

        /// <summary>
        /// Get a selected comment of a selected commit of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose commit comment you wish to get.</param>
        /// <param name="commitId">The Id of the commit whose comments you wish to get.</param>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public object GetCommitComment(string accountName, string repository, string commitId, int commentId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/comments/" + commitId + "/" + commentId + "/");
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        /// <summary>
        /// Approve the selected commit of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose commit you wish to approve.</param>
        /// <param name="commitId">The Id of the commit that you wish to approve.</param>
        /// <returns></returns>
        public object ApproveCommit(string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/approve/");
            return _sharpBucketV2.Post(new object(), overrideUrl);
        }

        /// <summary>
        /// Delete the approval of a selected commit of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose commit approval you wish to delete.</param>
        /// <param name="commitId">The Id of the commit whose approval you wish to delete.</param>
        /// <returns></returns>
        public object DeleteCommitApproval(string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/approve/");
            return _sharpBucketV2.Delete(new object(), overrideUrl);
        }

        /// <summary>
        /// List all the pull requests info for the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request info you wish to get.</param>
        /// <returns></returns>
        public PullRequestsInfo ListPullRequests(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Get(new PullRequestsInfo(), overrideUrl);
        }

        /// <summary>
        /// Add a new pull request to the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository to which you wish to add the pull request.</param>
        /// <param name="pullRequest">The pull request that you wish to add.</param>
        /// <returns></returns>
        public PullRequest PostPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        /// <summary>
        /// Update a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request you wish to update</param>
        /// <param name="pullRequest">The pull request that you wish to update.</param>
        /// <returns></returns>
        public PullRequest PutPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }
        /// <summary>
        /// Get a specific pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request that you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request that you wish to get.</param>
        /// <returns></returns>
        public PullRequest GetPullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
            return _sharpBucketV2.Get(new PullRequest(), overrideUrl);
        }

        /// <summary>
        /// List all the commits of a selected pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request commits you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request whose commits you wish to get.</param>
        /// <returns></returns>
        public CommitInfo ListPullRequestCommits(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
            return _sharpBucketV2.Get(new CommitInfo(), overrideUrl);
        }

        /// <summary>
        /// Approve a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request you wish to approve.</param>
        /// <param name="pullRequestId">The Id of the pull request that you wish to approve.</param>
        /// <returns></returns>
        public PullRequestInfo ApprovePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Post(new PullRequestInfo(), overrideUrl);
        }

        /// <summary>
        /// Remove the approval of the pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request approval you wish to remove.</param>
        /// <param name="pullRequestId">The Id of the pull request whose approval you wish to remove.</param>
        /// <returns></returns>
        public object RemovePullRequestApproval(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Delete(new PullRequestInfo(), overrideUrl);
        }

        /// <summary>
        /// Get diff for a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request diff you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request whose diff you wish to get.</param>
        /// <returns></returns>
        public object GetDiffForPullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
            return _sharpBucketV2.Get(new Object(), overrideUrl);
        }

        /// <summary>
        /// Get pull request log of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request log you wish to get.</param>
        /// <returns></returns>
        public ActivityInfo GetPullRequestLog(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        /// <summary>
        /// Get activity of a pull request of the repository..
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose activity of pull request you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request whose activity you wish to get.</param>
        /// <returns></returns>
        public ActivityInfo GetPullRequestActivity(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        /// <summary>
        /// Accept and merge a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request you wish to accept and merge.</param>
        /// <param name="pullRequestId">The Id of the pull request that you wish to accept and mege.</param>
        /// <returns></returns>
        public Merge AcceptAndMergePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
            return _sharpBucketV2.Post(new Merge(), overrideUrl);
        }

        /// <summary>
        /// Decline a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request you wish to decline.</param>
        /// <param name="pullRequestId">The Id of the pull request that you wish to decline.</param>
        /// <returns></returns>
        public Merge DeclinePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
            return _sharpBucketV2.Get(new Merge(), overrideUrl);
        }

        /// <summary>
        /// List all the comments of a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request comments you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request whose comments you wish to get.</param>
        /// <returns></returns>
        public object ListPullRequestComments(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        /// <summary>
        /// Get a comment of a pull request of the repository.
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repository">The repository whose pull request comment you wish to get.</param>
        /// <param name="pullRequestId">The Id of the pull request whose comment you wish to get.</param>
        /// <param name="commentId">The Id of the comment that you wish to get.</param>
        /// <returns></returns>
        public Comment GetPullRequestComment(string accountName, string repository, int pullRequestId, int commentId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
            return _sharpBucketV2.Get(new Comment(), overrideUrl);
        }
    }
}