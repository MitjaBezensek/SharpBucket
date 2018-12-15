using SharpBucket.V2.Pocos;
using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    public class TagResource
    {
        private readonly string _accountName;
        private readonly string _repository;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        /// <summary>
        /// Manage tags for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
        /// More info:
        /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/tags
        /// </summary>
        /// <returns></returns>
        public TagResource(string accountName, string repository, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName;
            _repository = repository;
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Tag> ListTags(int max = 0) => ListTags(null, null, max);

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="filter">The filter string to apply to the query.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Tag> ListTags(string filter, int max = 0) => ListTags(filter, null, max);

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="filter">The filter string to apply to the query.</param>
        /// <param name="sort">Name of the field to sort by.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Tag> ListTags(string filter, string sort, int max = 0)
        {
            return _repositoriesEndPoint.ListTags(_accountName, _repository, filter, sort, max);
        }
    }
}