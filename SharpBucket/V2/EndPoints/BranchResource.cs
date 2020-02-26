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
    public class BranchResource
    {
        private readonly string _accountName;
        private readonly string _slug;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public BranchResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        public List<Branch> ListBranches() => ListBranches(new ListParameters());

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Branch> ListBranches(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListBranches(_accountName, _slug, parameters);
        }

        /// <summary>
        /// Enumerate branches associated with a specific repository.
        /// </summary>
        public IEnumerable<Branch> EnumerateBranches() => EnumerateBranches(new EnumerateParameters());

        /// <summary>
        /// Enumerate branches associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the queries.</param>
        public IEnumerable<Branch> EnumerateBranches(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.EnumerateBranches(_accountName, _slug, parameters);
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
            return _repositoriesEndPoint.EnumerateBranchesAsync(_accountName, _slug, parameters, token);
        }
#endif

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        public void DeleteBranch(string branchName)
        {
            _repositoriesEndPoint.DeleteBranch(_accountName, _slug, branchName);
        }

        /// <summary>
        /// Removes a branch.
        /// </summary>
        /// <param name="branchName">The name of the branch to delete.</param>
        /// <param name="token">The cancellation token</param>
        public async Task DeleteBranchAsync(string branchName, CancellationToken token = default)
        {
            await _repositoriesEndPoint.DeleteBranchAsync(_accountName, _slug, branchName, token);
        }
    }
}