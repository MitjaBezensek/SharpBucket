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
        /// Get the Repositories End Point for a specific repository and account.
        /// The Repositories End Point is your main End Point for getting information of the
        /// specified repository. This includes information like: issues, issue comments, commits,...
        /// This end point contains access to a few other end points, like Issues End point.
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint+-+1.0
        /// </summary>
        /// <param name="accountName">The account that holds the specific repository.</param>
        /// <param name="repository">The repository of interest.</param>
        /// <returns></returns>
        public RepositoriesEndPointV1 Repositories(string accountName, string repository){
            return new RepositoriesEndPointV1(accountName, repository, this);
        }

        /// <summary>
        /// Get the User End Point.
        /// The User End Point is used for getting the information about the current user.
        /// This information includes things like: followers, privileges,...
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/user+Endpoint
        /// </summary>
        /// <returns></returns>
        public UserEndPointV1 User(){
            return new UserEndPointV1(this);
        }

        /// <summary>
        /// Get the Users End Point.
        /// The Users End Point is used for getting information about privileges, emails, ssh-key, consumers,...
        /// for a specified account.
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint+-+1.0
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public UsersEndpointV1 Users(string accountName){
            return new UsersEndpointV1(accountName, this);
        }

        /// <summary>
        /// Get the Privileges End Point.
        /// The Privileges End Point is used for getting all the information about privileges:
        /// user privileges, repository privileges,...
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/privileges+Endpoint
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public PrivilegesEndPointV1 Privileges(string accountName){
            return new PrivilegesEndPointV1(accountName, this);
        }
    }
}