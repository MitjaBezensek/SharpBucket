using SharpBucket.V2.Pocos;
using System.Collections.Generic;
using Serilog;

namespace SharpBucket.V2.EndPoints
{
    public class BranchResource
    {
        private readonly string _accountName;
        private readonly string _repository;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        /// <summary>
        /// Manage branches for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/branches
        /// </summary>
        /// <returns></returns>
        public BranchResource(string accountName, string repository, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Lists all branches associated with a specific repository.
        /// </summary>
        /// <returns></returns>
        public List<Branch> ListBranches()
        {
            return _repositoriesEndPoint.ListBranches(_accountName, _repository);
        }

        /// <summary>
        /// With Logging
        /// Returns a list of branches for the specific repository
        /// </summary>
        /// <returns></returns>
        public List<Branch> ListBranches(ILogger logger)
        {
            return _repositoriesEndPoint.ListBranches(logger, _accountName, _repository);
        }
        
        /// <summary>
        /// Posts a new branch for the specific repository
        /// </summary>
        /// <param name="branch"></param>
        public void PostBranch(Branch branch)
        {
            _repositoriesEndPoint.PostBranch(_accountName, _repository, branch);
        }

        /// <summary>
        /// With Logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="branch"></param>
        public void PostBranch(ILogger logger, Branch branch)
        {
            _repositoriesEndPoint.PostBranch(logger, _accountName, _repository, branch);
        }
    }
}