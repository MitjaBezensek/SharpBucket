using SharpBucket.V1.EndPoints;

namespace SharpBucket.V1{
    public sealed class SharpBucketV1 : SharpBucket{
        /// <summary>
        /// A client for the V1 of the BitBucketAPI.
        /// You can read more about the V1 of the API here:
        /// https://confluence.atlassian.com/display/BITBUCKET/Version+1
        /// </summary>
        public SharpBucketV1(){
            _baseUrl = "https://bitbucket.org/api/1.0/";
        }

        /// <summary>
        /// Get the Privileges End Point for a specific account.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the Privileges End Point.</param>
        /// <returns></returns>
        public PrivilegesEndPointV1 Privileges(string accountName){
            return new PrivilegesEndPointV1(accountName, this);
        }

        /// <summary>
        /// Get the Repositories End Point for a specific repository and account.
        /// </summary>
        /// <param name="accountName">The account that is the owner of the specific repository.</param>
        /// <param name="repository">The repository of interest.</param>
        /// <returns></returns>
        public RepositoriesEndPointV1 Repositories(string accountName, string repository){
            return new RepositoriesEndPointV1(accountName, repository, this);
        }

        /// <summary>
        /// Get the User End Point.
        /// </summary>
        /// <returns></returns>
        public UserEndPointV1 User(){
            return new UserEndPointV1(this);
        }

        /// <summary>
        /// Get the Users End Point for a specific account.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the Users End Point.</param>
        /// <returns></returns>
        public UsersEndPointV1 Users(string accountName){
            return new UsersEndPointV1(accountName, this);
        }
    }
}