namespace SharpBucket.V2.EndPoints{
    public class RepositoryEndPointV2{
        private string _accountName;
        private string _repository;
        private SharpBucketV2 _sharpBucketV2;

        public RepositoryEndPointV2(string accountName, string repository, SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _repository = repository;
            _accountName = accountName;
        }
    }
}