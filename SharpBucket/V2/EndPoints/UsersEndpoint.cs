using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The UsersEndPoint End Point gets a user account's profile information.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint
    /// </summary>
    public class UsersEndpoint : EndPoint
    {
        private readonly string _repositoriesUrl;

        public UsersEndpoint(string accountName, SharpBucketV2 sharpBucketV2) :
            base(sharpBucketV2, "users/" + accountName + "/")
        {
            _repositoriesUrl = "repositories/" + accountName + "/";
        }

        /// <summary>
        /// Gets the public information associated with a user. 
        /// If the user's profile is private, the caller must be authenticated as the account holder to view this information.  
        /// </summary>
        /// <returns></returns>
        public User GetProfile()
        {
            return _sharpBucketV2.Get(new User(), _baseUrl);
        }

        /// <summary>
        /// List all the accounts following the user.  
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<User> ListFollowers(int max = 0)
        {
            var overrideUrl = _baseUrl + "followers/";
            return GetPaginatedValues<User>(overrideUrl, max);
        }

        /// <summary>
        /// List all the accounts the user is following. 
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<User> ListFollowing(int max = 0)
        {
            var overrideUrl = _baseUrl + "following/";
            return GetPaginatedValues<User>(overrideUrl, max);
        }

        // TODO: Moved permanently
        /// <summary>
        /// List all of the user's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(int max = 0)
        {
            return GetPaginatedValues<Repository>(_repositoriesUrl, max);
        }
    }
}