using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class PrivilegesEndPointV1{
        private readonly string _baseUrl;
        private readonly SharpBucketV1 _sharpBucketV1;

        public PrivilegesEndPointV1(string accountName, SharpBucketV1 sharpBucketV1){
            _sharpBucketV1 = sharpBucketV1;
            _baseUrl = "privileges/" + accountName + "/";
        }

        public List<RepositoryPrivileges> ListRepositoryPrivileges(string repository){
            string overrideUrl = _baseUrl + repository + "/";
            return _sharpBucketV1.Get(new List<RepositoryPrivileges>(), overrideUrl);
        }

        public RepositoryPrivilegesUser GetPrivilegesForAccount(string repository, string accountName){
            string overrideUrl = _baseUrl + repository + "/" + accountName;
            return _sharpBucketV1.Get(new RepositoryPrivilegesUser(), overrideUrl);
        }
    }
}