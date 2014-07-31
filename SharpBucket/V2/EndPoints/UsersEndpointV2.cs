using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    public class UsersEndpointV2{
        private readonly SharpBucketV2 _sharpBucketV2;

        private readonly string _baseUrl;
        private readonly string _repositoriesUrl;

        public UsersEndpointV2(string accountName, SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "users/" + accountName + "/";
            _repositoriesUrl = "repositories/" + accountName + "/";
        }

        public User GetProfile(){
            return _sharpBucketV2.Get(new User(), _baseUrl);
        }

        public UsersInfo ListFollowers(){
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV2.Get(new UsersInfo(), overrideUrl);
        }

        public UsersInfo ListFollowing(){
            var overrideUrl = _baseUrl + "following/";
            return _sharpBucketV2.Get(new UsersInfo(), overrideUrl);
        }

        // Moved permanently
        public List<Repository> ListRepositories(){
            return _sharpBucketV2.Get(new List<Repository>(), _repositoriesUrl);
        }
    }
}