using System;
using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    public class RepositoriesEndPointV2{
        private readonly SharpBucketV2 _sharpBucketV2;
        private readonly string _baseUrl;

        public RepositoriesEndPointV2(SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "repositories/";
        }

        public PullRequestsEndPoint PullReqests(string accountName, string repository){
            return new PullRequestsEndPoint(accountName, repository, this);
        }

        public RepositoryEndPointV2 Repository(string accountName, string repository){
            return new RepositoryEndPointV2(accountName, repository, this);
        }

        public List<Repository> ListRepositories(string accountName){
            var overrideUrl = _baseUrl + accountName + "/";
            return _sharpBucketV2.Get(new RepositoryInfo(), overrideUrl).values;
        }

        public List<Repository> ListPublicRepositories(){
            return _sharpBucketV2.Get(new RepositoryInfo(), _baseUrl).values;
        }

        public Repository GetRepository(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Get(new Repository(), overrideUrl);
        }

        public Repository PutRepository(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Put(repo, overrideUrl);
        }

        public Repository DeleteRepository(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, null);
            return _sharpBucketV2.Delete(repo, overrideUrl);
        }

        private string GetRepositoryUrl(string accountName, string repository, string append){
            var format = _baseUrl + "{0}/{1}/{2}";
            return string.Format(format, accountName, repository, append);
        }

        public List<Watcher> ListWatchers(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "watchers");
            return _sharpBucketV2.Get(new WatcherInfo(), overrideUrl).values;
        }

        public List<Fork> ListForks(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "forks");
            return _sharpBucketV2.Get(new ForkInfo(), overrideUrl).values;
        }

        public List<BranchRestriction> ListBranchRestrictions(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Get(new BranchRestrictionInfo(), overrideUrl).values;
        }

        public BranchRestriction PostBranchRestriction(Repository repo, string accountName, string repository, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/");
            return _sharpBucketV2.Post(restriction, overrideUrl);
        }

        public BranchRestriction GetBranchRestriction(Repository repo, string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Get(new BranchRestriction(), overrideUrl);
        }

        public BranchRestriction PutBranchRestriction(Repository repo, string accountName, string repository, int restrictionId, BranchRestriction restriction){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Put(restriction, overrideUrl);
        }

        public BranchRestriction DeleteBranchRestriction(Repository repo, string accountName, string repository, int restrictionId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "branch-restrictions/" + restrictionId);
            return _sharpBucketV2.Delete(new BranchRestriction(), overrideUrl);
        }

        public object GetDiff(Repository repo, string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "diff/" + options);
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        public object GetPatch(Repository repo, string accountName, string repository, object options){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "patch/" + options);
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        public object ListCommits(Repository repo, string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/");
            return _sharpBucketV2.Get(repo, overrideUrl);
        }

        public object GetCommit(Repository repo, string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId);
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        public List<object> ListCommitComments(Repository repo, string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/comments/");
            return _sharpBucketV2.Get(new List<object>(), overrideUrl);
        }

        public object GetCommitComment(Repository repo, string accountName, string repository, string commitId, int commentId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/comments/" + commitId + "/");
            return _sharpBucketV2.Get(new object(), overrideUrl);
        }

        public object ApproveCommit(Repository repo, string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/approve/");
            return _sharpBucketV2.Post(new object(), overrideUrl);
        }

        public object DeleteCommitApproval(Repository repo, string accountName, string repository, string commitId){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "commits/" + commitId + "/approve/");
            return _sharpBucketV2.Delete(new object(), overrideUrl);
        }

        public List<PullRequest> ListPullRequests(string accountName, string repository){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Get(new PullRequestsInfo(), overrideUrl).values;
        }

        public PullRequest PostPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Post(pullRequest, overrideUrl);
        }

        public PullRequest PutPullRequest(string accountName, string repository, PullRequest pullRequest){
            var overrideUrl = GetRepositoryUrl(accountName, repository, "pullrequests/");
            return _sharpBucketV2.Put(pullRequest, overrideUrl);
        }
    }
}