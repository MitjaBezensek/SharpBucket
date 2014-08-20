using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints{
    /// <summary>
    /// The Users End Point gets a user account's profile information.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint
    /// </summary>
    public class UsersEndpoint{
        private readonly SharpBucketV2 _sharpBucketV2;

        private readonly string _baseUrl;
        private readonly string _repositoriesUrl;

        public UsersEndpoint(string accountName, SharpBucketV2 sharpBucketV2){
            _sharpBucketV2 = sharpBucketV2;
            _baseUrl = "users/" + accountName + "/";
            _repositoriesUrl = "repositories/" + accountName + "/";
        }

        /// <summary>
        /// Gets the public information associated with a user. 
        /// If the user's profile is private, the caller must be authenticated as the account holder to view this information.  
        /// </summary>
        /// <returns></returns>
        public User GetProfile(){
            return _sharpBucketV2.Get(new User(), _baseUrl);
        }

        /// <summary>
        /// List all the accounts following the user.  
        /// </summary>
        /// <returns></returns>
        public UsersInfo ListFollowers(){
            var overrideUrl = _baseUrl + "followers/";
            return _sharpBucketV2.Get(new UsersInfo(), overrideUrl);
        }

        /// <summary>
        /// List all the accounts the user is following. 
        /// </summary>
        /// <returns></returns>
        public UsersInfo ListFollowing(){
            var overrideUrl = _baseUrl + "following/";
            return _sharpBucketV2.Get(new UsersInfo(), overrideUrl);
        }

        // TODO: Moved permanently
        /// <summary>
        /// List all of the user's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <returns></returns>
        public List<Repository> ListRepositories(){
            return _sharpBucketV2.Get(new List<Repository>(), _repositoriesUrl);
        }
    }
}