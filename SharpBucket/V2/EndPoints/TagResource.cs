using SharpBucket.Utility;
using SharpBucket.V2.Pocos;
using System;
using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage tags for a repository. Use this resource to perform CRUD (create/read/update/delete) operations. 
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/refs/tags
    /// </summary>
    public class TagResource
    {
        private readonly string _accountName;
        private readonly string _slug;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public TagResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <returns></returns>
        public List<Tag> ListTags() => ListTags(new ListParameters());

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        public List<Tag> ListTags(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListTags(_accountName, _slug, parameters);
        }
    }
}