using SharpBucket.V1.EndPoints;

namespace SharpBucket.V1
{
    public sealed class SharpBucketV1 : SharpBucket
    {
        /// <summary>
        /// A client for the V1 of the BitBucketAPI.
        /// You can read more about the V1 of the API here:
        /// https://confluence.atlassian.com/display/BITBUCKET/Version+1
        /// </summary>
        public SharpBucketV1()
        {
            _baseUrl = "https://bitbucket.org/api/1.0/";
        }

        /// <summary>
        /// A client for the V1 of the BitBucketAPI.
        /// You can read more about the V1 of the API here:
        /// https://confluence.atlassian.com/display/BITBUCKET/Version+1
        /// </summary>
        /// <param name="baseUrl">If you are locally hosting your BitBucket repository
        /// you can use this constructor to set the url of your local Bitbucket server.
        /// All API requests will then use this url as a base. 
        /// </param>
        public SharpBucketV1(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Get the Privileges End Point for a specific account.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the Privileges End Point.</param>
        /// <returns></returns>
        public PrivilegesEndPoint PrivilegesEndPoint(string accountName)
        {
            return new PrivilegesEndPoint(accountName, this);
        }

        /// <summary>
        /// Get the Repositories End Point for a specific repository and account.
        /// </summary>
        /// <param name="accountName">The account that is the owner of the specific repository.</param>
        /// <param name="repository">The repository of interest.</param>
        /// <returns></returns>
        public RepositoriesEndPoint RepositoriesEndPoint(string accountName, string repository)
        {
            return new RepositoriesEndPoint(accountName, repository, this);
        }

        /// <summary>
        /// Use the user endpoints to gets information related to the currently authenticated user. 
        /// It is useful for OAuth or other in situations where the username is unknown. 
        /// This endpoint returns information about an individual or team account. 
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/user+Endpoint
        /// </summary>
        /// <returns></returns>
        public UserEndPoint UserEndPoint()
        {
            return new UserEndPoint(this);
        }

        /// <summary>
        /// Get the Users End Point for a specific account.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the Users End Point.</param>
        /// <returns></returns>
        public UsersEndPoint UsersEndPoint(string accountName)
        {
            return new UsersEndPoint(accountName, this);
        }

        /// <summary>
        /// The groups endpoint provides functionality for querying information about user groups, 
        /// creating new ones, updating memberships, and deleting them. Both individual and team accounts can define groups.
        /// To manage group information on an individual account, the caller must authenticate with administrative rights on the account. 
        /// More info:
        /// https://confluence.atlassian.com/bitbucket/groups-endpoint-296093143.html
        /// </summary>
        public GroupsEndPoint GroupsEndPoint(string accountName)
        {
            return new GroupsEndPoint(accountName, this);
        }
    }
}