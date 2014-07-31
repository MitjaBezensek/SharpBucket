namespace SharpBucket.V2.EndPoints {
    public class PullRequestsEndPoint {
        private RepositoriesEndPointV2 _repositoriesEndPointV2;
        private string _repository;
        private string _accountName;

        public PullRequestsEndPoint(string accountName, string repository, RepositoriesEndPointV2 repositoriesEndPointV2){
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPointV2 = repositoriesEndPointV2;
        }
    }
}
