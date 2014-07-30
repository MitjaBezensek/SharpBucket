using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2{
    public sealed class SharpBucketV2 : SharpBucket{
        public SharpBucketV2(){
            _baseUrl = "https://bitbucket.org/api/2.0/";
        }

        public TeamsEndPointV2 Teams(){
            return new TeamsEndPointV2(this);
        }

        public RepositoryEndPointV2 Repository(string accountName, string repository){
            return new RepositoryEndPointV2(accountName, repository, this);
        }

        public UsersEndpointV2 Users(string accountName){
            return new UsersEndpointV2(accountName, this);
        }
    }
}