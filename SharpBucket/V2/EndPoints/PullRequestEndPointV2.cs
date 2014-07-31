namespace SharpBucket.V2.EndPoints{
    public class PullRequestEndPointV2{
        private readonly int _pullRequestId;
        private readonly RepositoriesEndPointV2 _repositoriesEndPointV2;
        private readonly string _repository;
        private readonly string _accountName;

        public PullRequestEndPointV2(string accountName, string repository, int pullRequestId, RepositoriesEndPointV2 repositoriesEndPointV2){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
            _pullRequestId = pullRequestId;
        }

        public object GetPullRequest(){
            return _repositoriesEndPointV2.GetPullRequest(_accountName, _repository, _pullRequestId);
        }

        public object ListPullRequestCommits(){
            return _repositoriesEndPointV2.ListPullRequestCommits(_accountName, _repository, _pullRequestId);
        }

        public object ApprovePullRequest(){
            return _repositoriesEndPointV2.ApprovePullRequest(_accountName, _repository, _pullRequestId);
        }

        public object RemovePullRequestApproval(){
            return _repositoriesEndPointV2.RemovePullRequestApproval(_accountName, _repository, _pullRequestId);
        }

        public object GetDiffForPullRequest(){
            return _repositoriesEndPointV2.GetDiffForPullRequest(_accountName, _repository, _pullRequestId);
        }

        public object GetPullRequestActivity(){
            return _repositoriesEndPointV2.GetPullRequestActivity(_accountName, _repository, _pullRequestId);
        }

        public object AcceptAndMergePullRequest(){
            return _repositoriesEndPointV2.AcceptAndMergePullRequest(_accountName, _repository, _pullRequestId);
        }

        public object DeclinePullRequest(){
            return _repositoriesEndPointV2.DeclinePullRequest(_accountName, _repository, _pullRequestId);
        }

        public object ListPullRequestComments(){
            return _repositoriesEndPointV2.ListPullRequestComments(_accountName, _repository, _pullRequestId);
        }

        public object GetPullRequestComment(int commentId){
            return _repositoriesEndPointV2.GetPullRequestComment(_accountName, _repository, _pullRequestId, commentId);
        }
    }
}