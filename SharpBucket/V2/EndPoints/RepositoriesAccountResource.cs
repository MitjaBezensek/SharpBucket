using System;
using System.Collections.Generic;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Class that implement the access to the repositories of a specified account.
    /// See: https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D
    /// </summary>
    public class RepositoriesAccountResource
    {
        private readonly ISharpBucketRequesterV2 _sharpBucketV2;
        private readonly string _accountName;
        private readonly string _baseUrl;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public RepositoriesAccountResource(
            ISharpBucketRequesterV2 sharpBucketV2, string accountName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _sharpBucketV2 = sharpBucketV2;
            _accountName = accountName.GuidOrValue();
            _baseUrl = $"repositories/{_accountName}/";
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// List of repositories associated with the account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        public List<Repository> ListRepositories()
            => ListRepositories(new ListRepositoriesParameters());

        /// <summary>
        /// List of repositories associated with the account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Repository> ListRepositories(ListRepositoriesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.GetPaginatedValues<Repository>(
                _baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate repositories associated with the account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        public IEnumerable<Repository> EnumerateRepositories()
            => EnumerateRepositories(new EnumerateRepositoriesParameters());

        /// <summary>
        /// Enumerate repositories associated with the account. If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Repository> EnumerateRepositories(EnumerateRepositoriesParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValues<Repository>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate repositories associated with the account, doing requests page by page.
        /// If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<Repository> EnumerateRepositoriesAsync(CancellationToken token = default)
            => EnumerateRepositoriesAsync(new EnumerateRepositoriesParameters(), token);

        /// <summary>
        /// Enumerate repositories associated with the account, doing requests page by page.
        /// If the caller is properly authenticated and authorized, 
        /// this method returns a collection containing public and private repositories. 
        /// Otherwise, this method returns a collection of the public repositories.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        public IAsyncEnumerable<Repository> EnumerateRepositoriesAsync(
            EnumerateRepositoriesParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));

            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Repository>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Use this resource to get information associated with an individual repository.
        /// You can use these calls with public or private repositories. 
        /// Private repositories require the caller to authenticate with an account that has the appropriate
        /// authorization.
        /// More info:
        /// https://confluence.atlassian.com/display/BITBUCKET/repository+Resource
        /// </summary>
        /// <param name="repoSlugOrName">The repository slug, name, or UUID.</param>
        public RepositoryResource RepositoryResource(string repoSlugOrName)
        {
            return new RepositoryResource(_accountName, repoSlugOrName, _repositoriesEndPoint);
        }
    }
}
