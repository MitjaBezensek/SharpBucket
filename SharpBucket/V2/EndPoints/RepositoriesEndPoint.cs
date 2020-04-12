using System;
using System.Collections.Generic;
using System.Threading;
using SharpBucket.Utility;
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
            : base(sharpBucketV2, "repositories/")
        {
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(int max = 0)
        {
            return GetPaginatedValues<Repository>(_baseUrl, max);
        }

        /// <summary>
        /// List of all the public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <returns></returns>
        public List<Repository> ListPublicRepositories(ListPublicRepositoriesParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return GetPaginatedValues<Repository>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all public repositories on Bitbucket.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Repository> EnumeratePublicRepositories(EnumeratePublicRepositoriesParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValues<Repository>(_baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all public repositories on BitBucket, doing requests page by page.
        /// </summary>m>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Repository> EnumeratePublicRepositoriesAsync(EnumeratePublicRepositoriesParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(_baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        #endregion

        #region Repositories Account Resource

        /// <summary>
        /// Gets the <see cref="RepositoriesAccountResource"/> that will allow to list the repositories of a specified
        /// account.
        /// </summary>
        /// <param name="accountName"The account whose repositories you wish to get.></param>
        public RepositoriesAccountResource RepositoriesResource(string accountName)
        {
            return new RepositoriesAccountResource(_sharpBucketV2, accountName, this);
        }

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <returns></returns>
        [Obsolete("Prefer go through the RepositoriesAccountResource(accountName) method.")]
        public List<Repository> ListRepositories(string accountName)
            => RepositoriesResource(accountName).ListRepositories();

        /// <summary>
        /// List of repositories associated with an account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="accountName">The account whose repositories you wish to get.</param>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        [Obsolete("Prefer go through the RepositoriesAccountResource(accountName) method, and use a method using a ListRepositoriesParameters.")]
        public List<Repository> ListRepositories(string accountName, ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            var overrideUrl = $"{_baseUrl}{accountName.GuidOrValue()}/";
            return GetPaginatedValues<Repository>(overrideUrl, parameters.Max, parameters.ToDictionary());
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
            return new RepositoryResource(accountName, repoSlugOrName, this);
        }

        #endregion

        #region Pull Requests Resource

        /// <summary>
        /// Manage pull requests for a repository. Use this resource to perform CRUD (create/read/update/delete) operations on a pull request. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/pullrequests
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        /// <returns></returns>
        [Obsolete("Use RepositoryResource(string,string).PullRequestsResource() instead")]
        public PullRequestsResource PullRequestsResource(string accountName, string repoSlugOrName)
        {
            return RepositoryResource(accountName, repoSlugOrName).PullRequestsResource();
        }

        #endregion

        #region Branch Resource

        /// <summary>
        /// Manage branches for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/branches
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        [Obsolete("Prefer RepositoryResource(accountName, repoSlugOrName).BranchesResource.")]
        public BranchResource BranchResource(string accountName, string repoSlugOrName)
        {
            return RepositoryResource(accountName, repoSlugOrName).BranchesResource;
        }

        #endregion

        #region Tag Resource

        /// <summary>
        /// Manage tags for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/tags
        /// </summary>
        /// <param name="accountName">The owner of the repository.</param>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        [Obsolete("Use RepositoryResource(string,string).TagsResource instead")]
        public TagResource TagResource(string accountName, string repoSlugOrName)
        {
            return new TagResource(accountName, repoSlugOrName, this);
        }

        #endregion
    }
}