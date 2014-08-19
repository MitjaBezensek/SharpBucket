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

        /// <summary>
        /// List the privileges for the specified repository.
        /// </summary>
        /// <param name="repository">The repository whose privileges you wish to get.</param>
        /// <returns></returns>
        public List<RepositoryPrivileges> ListRepositoryPrivileges(string repository){
            string overrideUrl = _baseUrl + repository + "/";
            return _sharpBucketV1.Get(new List<RepositoryPrivileges>(), overrideUrl);
        }

        /// <summary>
        /// List the privileges for a specific account for a specific repository.
        /// </summary>
        /// <param name="repository">The repository whose privileges you wish to get.</param>
        /// <param name="accountName">The account name whose privileges you wish to get.</param>
        /// <returns></returns>
        public RepositoryPrivilegesUser GetPrivilegesForAccount(string repository, string accountName){
            string overrideUrl = _baseUrl + repository + "/" + accountName;
            return _sharpBucketV1.Get(new RepositoryPrivilegesUser(), overrideUrl);
        }
    }
}