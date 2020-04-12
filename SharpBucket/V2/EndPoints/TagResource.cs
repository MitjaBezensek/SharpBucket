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
    [Obsolete("Use TagsResource instead")]
    public class TagResource
    {
        private readonly TagsResource _tagsResource;

        public TagResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _tagsResource = repositoriesEndPoint
                .RepositoriesResource(accountName)
                .RepositoryResource(repoSlugOrName)
                .TagsResource;
        }

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <returns></returns>
        public List<Tag> ListTags()
            => ListTags(new ListParameters());

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <returns></returns>
        public List<Tag> ListTags(ListParameters parameters)
            => _tagsResource.ListTags(parameters);
    }
}