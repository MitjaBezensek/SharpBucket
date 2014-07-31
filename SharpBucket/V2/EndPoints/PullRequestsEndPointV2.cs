using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints {
    public class PullRequestsEndPointV2 {
        private readonly RepositoriesEndPointV2 _repositoriesEndPointV2;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestsEndPointV2(string accountName, string repository, RepositoriesEndPointV2 repositoriesEndPointV2){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
        }

        public PullRequestEndPointV2 PullRequestEndPoint(int pullRequestId){
            return new PullRequestEndPointV2(_accountName, _repository, pullRequestId, _repositoriesEndPointV2);
        }

        public PullRequestsInfo ListPullRequests() {
            return _repositoriesEndPointV2.ListPullRequests(_accountName, _repository);
        }

        public object PostPullRequest(PullRequest pullRequest){
            return _repositoriesEndPointV2.PostPullRequest(_accountName, _repository, pullRequest);
        }

        public object PutPullRequest(PullRequest pullRequest){
            return _repositoriesEndPointV2.PutPullRequest(_accountName, _repository, pullRequest);
        }

        public object GetPullRequest(int pullRequestId){
            return _repositoriesEndPointV2.GetPullRequest(_accountName, _repository, pullRequestId);
        }

        public object ListPullRequestCommits(int pullRequestId){
            return _repositoriesEndPointV2.ListPullRequestCommits(_accountName, _repository, pullRequestId);
        }


        public object ApprovePullRequest(int pullRequestId){
            return _repositoriesEndPointV2.ApprovePullRequest(_accountName, _repository, pullRequestId);
        }

        public object RemovePullRequestApproval(int pullRequestId){
            return _repositoriesEndPointV2.RemovePullRequestApproval(_accountName, _repository, pullRequestId);
        }

        public object GetDiffForPullRequest(int pullRequestId){
            return _repositoriesEndPointV2.GetDiffForPullRequest(_accountName, _repository, pullRequestId);
        }

        public object GetPullRequestLog(){
            return _repositoriesEndPointV2.GetPullRequestLog(_accountName, _repository);
        }

        public object GetPullRequestActivity(int pullRequestId) {
            return _repositoriesEndPointV2.GetPullRequestActivity(_accountName, _repository, pullRequestId);
        }

        public object AcceptAndMergePullRequest(int pullRequestId) {
            return _repositoriesEndPointV2.AcceptAndMergePullRequest(_accountName, _repository, pullRequestId);
        }

        public object DeclinePullRequest(int pullRequestId) {
            return _repositoriesEndPointV2.DeclinePullRequest(_accountName, _repository, pullRequestId);
        }

        public object ListPullRequestComments(int pullRequestId) {
            return _repositoriesEndPointV2.ListPullRequestComments(_accountName, _repository, pullRequestId);
        }

        public object GetPullRequestComment(int pullRequestId, int commentId) {
            return _repositoriesEndPointV2.GetPullRequestComment(_accountName, _repository, pullRequestId, commentId);
        }
    }
}
