using SharpBucket.Utility;
using SharpBucket.V2.Pocos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage branches for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/branches
    /// </summary>
    public class BranchResource : EndPoint
    {
        [Obsolete("Prefer repositoriesEndPoint.RepositoryResource(accountName, repoSlugOrName).BranchesResource")]
        public BranchResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
            :this(repositoriesEndPoint.RepositoryResource(accountName, repoSlugOrName))
        {
        }

        internal BranchResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "refs/branches/")
        {
        }

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        public List<Branch> ListBranches()
            => ListBranches(new ListParameters());

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Branch> ListBranches(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<Branch>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Creates a new branch in the specified repository.
        /// </summary>
        /// <param name="branch">The branch to create.</param>
        /// <returns>The created branch.</returns>
        public Branch PostBranch(Branch branch)
        {
            return _sharpBucketV2.Post(branch, _baseUrl);
        }

        /// <summary>
        /// Enumerate branches associated with a specific repository.
        /// </summary>
        public IEnumerable<Branch> EnumerateBranches()
            => EnumerateBranches(new EnumerateParameters());

        /// <summary>
        /// Enumerate branches associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Branch> EnumerateBranches(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<Branch>(_baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate branches associated with a specific repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Branch> EnumerateBranchesAsync(CancellationToken token = default)
            => EnumerateBranchesAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate branches associated with a specific repository asynchronously, doing requests page by page.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public IAsyncEnumerable<Branch> EnumerateBranchesAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Branch>(_baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        public void DeleteBranch(string branchName)
        {
            var overrideUrl = _baseUrl + branchName;
            _sharpBucketV2.Delete(overrideUrl);
        }

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        /// <param name="token">The cancellation token</param>
        public Task DeleteBranchAsync(string branchName, CancellationToken token = default)
        {
            var overrideUrl = _baseUrl + branchName;
            return _sharpBucketV2.DeleteAsync(overrideUrl, token);
        }
    }
}