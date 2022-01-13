using System;
using System.Collections.Generic;
using System.Threading;

using Repository = SharpBucket.V2.Pocos.Repository;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The repositories endpoint has a number of resources you can use to manage repository resources. 
    /// For all repository resources, you supply a  repo_slug that identifies the specific repository.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories
    /// </summary>
    public class RepositoriesEndPoint : EndPoint
    {
        #region Repositories End Point

        public RepositoriesEndPoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "repositories")
        {
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(int max = 0)
        {
            return GetPaginatedValues<Repository>(BaseUrl, max);
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(ListPublicRepositoriesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return GetPaginatedValues<Repository>(BaseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Repository> EnumeratePublicRepositories(EnumeratePublicRepositoriesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return SharpBucketV2.EnumeratePaginatedValues<Repository>(BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all public repositories on Bitbucket, doing requests page by page.
        /// </summary>m>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Repository> EnumeratePublicRepositoriesAsync(EnumeratePublicRepositoriesParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return SharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        #endregion

        #region Repositories Account Resource

        /// <summary>
        /// Gets the <see cref="RepositoriesAccountResource"/> that will allow to list the repositories of a specified
        /// account.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.></param>
        public RepositoriesAccountResource RepositoriesResource(string accountName)
        {
            return new RepositoriesAccountResource(this, accountName);
        }

        #endregion

        #region Repository resource

        /// <summary>
        /// Use this resource to get information associated with an individual repository. You can use these calls with public or private repositories. 
        /// Private repositories require the caller to authenticate with an account that has the appropriate authorization.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        /// <returns></returns>
        public RepositoryResource RepositoryResource(string accountName, string repoSlugOrName)
        {
            return RepositoriesResource(accountName).RepositoryResource(repoSlugOrName);
        }

        #endregion
    }
}