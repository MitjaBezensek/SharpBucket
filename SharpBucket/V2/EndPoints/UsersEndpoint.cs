using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
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
        private readonly Lazy<RepositoriesAccountResource> _repositoriesResource;

        public UsersEndpoint(string accountName, ISharpBucketRequesterV2 sharpBucketV2) :
            base(sharpBucketV2, $"users/{accountName.GuidOrValue()}/")
        {
            _repositoriesResource = new Lazy<RepositoriesAccountResource>(
                () => new RepositoriesEndPoint(sharpBucketV2).RepositoriesResource(accountName));
        }

        /// <summary>
        /// Gets the <see cref="RepositoriesAccountResource"/> corresponding to the account of this endpoint.
        /// </summary>
        /// <remarks>
        /// The /users/{username}/repositories request redirect to the /repositories/{username} request
        /// It's why providing here a shortcut to the /repositories/{username} resource is valid and equivalent.
        /// </remarks>
        public RepositoriesAccountResource RepositoriesResource => _repositoriesResource.Value;

        /// <summary>
        /// Gets the public information associated with a user. 
        /// If the user's profile is private, the caller must be authenticated as the account holder to view this information.  
        /// </summary>
        /// <returns></returns>
        public User GetProfile()
        {
            return _sharpBucketV2.Get<User>(_baseUrl);
        }

        /// <summary>
        /// Gets the public information associated with a user. 
        /// If the user's profile is private, the caller must be authenticated as the account holder to view this information.  
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<User> GetProfileAsync(CancellationToken token = default)
        {
            return await _sharpBucketV2.GetAsync<User>(_baseUrl, token);
        }

        /// <summary>
        /// List all the accounts following the user.  
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("The end point as been removed. See https://developer.atlassian.com/cloud/bitbucket/bitbucket-api-changes-gdpr/ for more details.")]
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
        [Obsolete("The end point as been removed. See https://developer.atlassian.com/cloud/bitbucket/bitbucket-api-changes-gdpr/ for more details.")]
        public List<User> ListFollowing(int max = 0)
        {
            var overrideUrl = _baseUrl + "following/";
            return GetPaginatedValues<User>(overrideUrl, max);
        }

        /// <summary>
        /// List all of the user's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        [Obsolete("Prefer go through the RepositoriesResource property.")]
        public List<Repository> ListRepositories(int max = 0)
            => this.RepositoriesResource.ListRepositories(new ListRepositoriesParameters { Max = max });
    }
}