using System.Collections.Generic;
using SharpBucket.V1.Pocos;
using SharpBucket.V1.Routes;

namespace SharpBucket.V1.EndPoints{
    public class UserEndPointV1{
        private readonly SharpBucketV1 _sharpBucketV1;

        public UserEndPointV1(SharpBucketV1 sharpBucketV1){
            _sharpBucketV1 = sharpBucketV1;
        }

        public UserInfo GetInfo(){
            return _sharpBucketV1.Get(new UserRoutes.GetInfo());
        }

        public string GetPrivileges(){
            return _sharpBucketV1.Get(new UserRoutes.GetPrivileges());
        }

        public List<Repository> ListFollows(){
            return _sharpBucketV1.Get(new UserRoutes.ListFollows());
        }

        public List<Repository> ListRepositories(){
            return _sharpBucketV1.Get(new UserRoutes.ListRepositories());
        }

        public RepositoriesOverview RepositoriesOverview(){
            return _sharpBucketV1.Get(new UserRoutes.GetRepositoryOverview());
        }

        public string GetRepositoryDasboard(){
            return _sharpBucketV1.Get(new UserRoutes.GetRepositoryDashboard());
        }
    }
}