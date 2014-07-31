using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    public class RepositoryEndPointV2{
        private RepositoriesEndPointV2 _repositoriesEndPointV2;
        private string _accountName;
        private string _repository;
        private readonly string _baseUrl;

        public RepositoryEndPointV2(string accountName, string repository, RepositoriesEndPointV2 repositoriesEndPointV2){
            _repository = repository;
            _accountName = accountName;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
            _baseUrl = string.Format("repositories/{0}/{1}/", accountName, repository);
        }

        public PullRequestsEndPointV2 PullRequests(){
            return new PullRequestsEndPointV2(_accountName, _repository, _repositoriesEndPointV2);
        }

        public Repository DeleteRepository(){
            return _repositoriesEndPointV2.DeleteRepository(new Repository(), _accountName, _repository);
        }

        public List<Watcher> ListWatchers(){
            return _repositoriesEndPointV2.ListWatchers(_accountName, _repository);
        }

        public List<Fork> ListForks(){
            return _repositoriesEndPointV2.ListForks(new Repository(), _accountName, _repository);
        }

        public object ListBranchRestrictions(){
            return _repositoriesEndPointV2.ListBranchRestrictions(new Repository(), _accountName, _repository);
        }

        public object GetBranchRestriction(int restrictionId) {
            return _repositoriesEndPointV2.GetBranchRestriction(new Repository(), _accountName, _repository, restrictionId);
        }

        public BranchRestriction PostBranchRestriction(BranchRestriction restriction){
            return _repositoriesEndPointV2.PostBranchRestriction(new Repository(), _accountName, _repository, restriction);
        }

        public object DeleteBrachRestriction(int restrictionId){
            return _repositoriesEndPointV2.DeleteBranchRestriction(new Repository(), _accountName, _repository, restrictionId);
        }

        public object GetDiff(object options){
            return _repositoriesEndPointV2.GetDiff(new Repository(), _accountName, _repository, options);
        }

        public object GetPatch(object options){
            return _repositoriesEndPointV2.GetPatch(new Repository(), _accountName, _repository, options);
        }

        public object ListCommits(){
            return _repositoriesEndPointV2.ListCommits(new Repository(), _accountName, _repository);
        }

        public object GetCommit(string commitId){
            return _repositoriesEndPointV2.GetCommit(new Repository(), _accountName, _repository, commitId);
        }

        public object ListCommitComments(string commitId){
            return _repositoriesEndPointV2.ListCommitComments(new Repository(), _accountName, _repository, commitId);
        }

        public object GetCommitComment(string commitId, int commentId){
            return _repositoriesEndPointV2.GetCommitComment(new Repository(), _accountName, _repository, commitId, commentId);
        }

        public object ApproveCommit(string commitId){
            return _repositoriesEndPointV2.ApproveCommit(new Repository(), _accountName, _repository, commitId);
        }

        public object DeleteCommitApproval(string commitId){
            return _repositoriesEndPointV2.DeleteCommitApproval(new Repository(), _accountName, _repository, commitId);
        }
    }
}