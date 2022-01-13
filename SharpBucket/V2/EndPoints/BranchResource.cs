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
        internal BranchResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "refs/branches")
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
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<Branch>(BaseUrl, parameters.Max, parameters.ToDictionary());
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
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValues<Branch>(BaseUrl, parameters.ToDictionary(), parameters.PageLen);
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
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Branch>(BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Creates a new branch in the specified repository.
        /// </summary>
        /// <param name="branch">The branch to create.</param>
        /// <returns>The created branch.</returns>
        public Branch PostBranch(Branch branch)
        {
            return SharpBucketV2.Post(branch, BaseUrl);
        }

        /// <summary>
        /// Creates a new branch in the specified repository.
        /// </summary>
        /// <param name="branch">The branch to create.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The created branch.</returns>
        public Task<Branch> PostBranchAsync(Branch branch, CancellationToken token = default)
        {
            return SharpBucketV2.PostAsync(branch, BaseUrl, token);
        }

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        public void DeleteBranch(string branchName)
        {
            var overrideUrl = BaseUrl + "/" + branchName;
            SharpBucketV2.Delete(overrideUrl);
        }

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        /// <param name="token">The cancellation token</param>
        public Task DeleteBranchAsync(string branchName, CancellationToken token = default)
        {
            var overrideUrl = BaseUrl + "/" + branchName;
            return SharpBucketV2.DeleteAsync(overrideUrl, token);
        }
    }
}