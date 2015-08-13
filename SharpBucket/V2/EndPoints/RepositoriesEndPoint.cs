using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class RepositoriesEndPoint : EndPoint {


        #region Repositories End Point

        public RepositoriesEndPoint(SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "repositories/";
        }

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories. 
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(string accountName){
            var overrideUrl = _baseUrl + accountName + "/";
            return GetAllValues<Repository, RepositoryInfo>(overrideUrl);
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.  This produces a paginated response. 
        /// Pagination only goes forward (it's not possible to navigate to previous pages) and navigation is done by following the URL for the next page.
        /// The returned repositories are ordered by creation date, oldest repositories first. Only public repositories are returned.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(){
            return GetAllValues<Repository, RepositoryInfo>(_baseUrl);
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
        public RepositoryResource RepositoryResource(string accountName, string repository){
            return new RepositoryResource(accountName, repository, this);
        }

        internal Repository GetRepository(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Get(new Repository(), overrideUrl);
        }

        internal Repository PutRepository(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        internal Repository DeleteRepository(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(new Repository(), overrideUrl);
        }

        private string GetRepositoryUrl(string accountName, string repository, string append){
            var format = _baseUrl + "{0}/{1}/{2}";
            return string.Format(format, accountName, repository, append);
        }

        internal List<Watcher> ListWatchers(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
            return _sharpBucketV2.Get(new WatcherInfo(), overrideUrl).values;
        }

        internal List<Fork> ListForks(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return _sharpBucketV2.Get(new ForkInfo(), overrideUrl).values;
        }

        #endregion

        #region Pull Requests Resource

        public PullRequestsResource PullReqestsResource(string accountName, string repository){
            return new PullRequestsResource(accountName, repository, this);
        }

        internal PullRequestsInfo ListPullRequests(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Get(new PullRequestsInfo(), overrideUrl);
        }

        internal PullRequest PostPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        internal PullRequest PutPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }

        internal ActivityInfo GetPullRequestLog(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/activity/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        #endregion

        #region Pull Request Resource

        internal PullRequest GetPullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/");
            return _sharpBucketV2.Get(new PullRequest(), overrideUrl);
        }

        internal CommitInfo ListPullRequestCommits(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/commits/");
            return _sharpBucketV2.Get(new CommitInfo(), overrideUrl);
        }

        internal PullRequestInfo ApprovePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Post(new PullRequestInfo(), overrideUrl);
        }

        internal object RemovePullRequestApproval(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/approve/");
            return _sharpBucketV2.Delete(new PullRequestInfo(), overrideUrl);
        }

        internal object GetDiffForPullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/diff/");
            return _sharpBucketV2.Get(new Object(), overrideUrl);
        }

        internal ActivityInfo GetPullRequestActivity(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/activity/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        internal Merge AcceptAndMergePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/merge/");
            return _sharpBucketV2.Post(new Merge(), overrideUrl);
        }

        internal Merge DeclinePullRequest(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/decline/");
            return _sharpBucketV2.Get(new Merge(), overrideUrl);
        }

        internal object ListPullRequestComments(string accountName, string repository, int pullRequestId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/");
            return _sharpBucketV2.Get(new ActivityInfo(), overrideUrl);
        }

        internal Comment GetPullRequestComment(string accountName, string repository, int pullRequestId, int commentId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/" + pullRequestId + "/comments/" + commentId + "/");
            return _sharpBucketV2.Get(new Comment(), overrideUrl);
        }

        #endregion

        #region Branch Restrictions resource

        internal List<BranchRestriction> ListBranchRestrictions(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            var branchRestrictions = _sharpBucketV2.Get(new BranchRestrictionInfo(), overrideUrl);
            return branchRestrictions == null ? null : branchRestrictions.values;
        }

        internal BranchRestriction PostBranchRestriction(string accountName, string repository, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        internal BranchRestriction GetBranchRestriction(string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(new BranchRestriction(), overrideUrl);
        }

        internal BranchRestriction PutBranchRestriction(string accountName, string repository, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restriction.id);
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        internal BranchRestriction DeleteBranchRestriction(string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(new BranchRestriction(), overrideUrl);
        }

        #endregion

        #region Diff resource

        internal object GetDiff(string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        internal object GetPatch(string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        #endregion

        #region Commits Resource

        internal CommitInfo ListCommits(string accountName, string repository, string branchortag = null ){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
            if ( !string.IsNullOrEmpty( branchortag )){
                overrideUrl += branchortag;
            }
            return _sharpBucketV2.Get(new CommitInfo(), overrideUrl);
        }

        internal Commit GetCommit(string accountName, string repository, string revision){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commit/" + revision);
            return _sharpBucketV2.Get(new Commit(), overrideUrl);
        }

        internal List<object> ListCommitComments(string accountName, string repository, string revision){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/");
            return _sharpBucketV2.Get(new List<object>(), overrideUrl);
        }

        internal object GetCommitComment(string accountName, string repository, string revision, int commentId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/comments/" + revision + "/" + commentId + "/");
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        internal object ApproveCommit(string accountName, string repository, string revision){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Post(new object(), overrideUrl);
        }

        internal object DeleteCommitApproval(string accountName, string repository, string revision){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + revision + "/approve/");
            return _sharpBucketV2.Delete(new object(), overrideUrl);
        }

        #endregion
    }
}