using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2{
    public sealed class SharpBucketV2 : SharpBucket{
        public SharpBucketV2(){
            _baseUrl = "https://bitbucket.org/api/2.0/";
        }

        public TeamsEndPointV2 Teams(string teamName){
            return new TeamsEndPointV2(this, teamName);
        }

        public RepositoriesEndPointV2 Repositories(){
            return new RepositoriesEndPointV2(this);
        }

        public UsersEndpointV2 Users(string accountName){
            return new UsersEndpointV2(accountName, this);
        }
    }
}