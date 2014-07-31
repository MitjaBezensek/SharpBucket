using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    public class TeamsEndPointV2{
        private SharpBucketV2 _sharpBucketV2;
        private string _teamName;
        private string _baseUrl;

        public TeamsEndPointV2(SharpBucketV2 sharpBucketV2, string teamName){
            _teamName = teamName;
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "teams/" + _teamName + "/";
        }

        public Team GetProfile(){
            return _sharpBucketV2.Get(new Team(), _baseUrl);
        }

        public User ListMembers(){
            var overrideUrl = _baseUrl + "members/";;
            return _sharpBucketV2.Get(new User(), overrideUrl);
        }

        public TeamProfile ListFollowers() {
            var overrideUrl = _baseUrl + "followers/"; ;
            return _sharpBucketV2.Get(new TeamProfile(), overrideUrl);
        }

        public TeamProfile ListFollowing() {
            var overrideUrl = _baseUrl + "following/"; ;
            return _sharpBucketV2.Get(new TeamProfile(), overrideUrl);
        }
        public List<Repository> ListRepositories() {
            var overrideUrl = _baseUrl + "repositories/"; ;
            return _sharpBucketV2.Get(new List<Repository>(), overrideUrl);
        }
    }
}