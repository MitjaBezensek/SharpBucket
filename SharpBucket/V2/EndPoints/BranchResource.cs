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
        /// <returns></returns>
        public List<Branch> ListBranches() => ListBranches(new ListParameters());

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        public List<Branch> ListBranches(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListBranches(_accountName, _slug, parameters);
        }

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
        public async Task DeleteBranchAsync(string branchName, CancellationToken token = default(CancellationToken))
        {
            await _repositoriesEndPoint.DeleteBranchAsync(_accountName, _slug, branchName, token: token);
        }
    }
}