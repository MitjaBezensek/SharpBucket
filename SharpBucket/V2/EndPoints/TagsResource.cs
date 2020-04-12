using System;
using System.Collections.Generic;
using System.Threading;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage tags for a repository.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/refs/tags
    /// </summary>
    public class TagsResource : EndPoint
    {
        [Obsolete("Prefer repositoriesEndPoint.RepositoriesResource(accountName).RepositoryResource(repoSlugOrName).TagsResource")]
        public TagsResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
            :this (repositoriesEndPoint.RepositoriesResource(accountName).RepositoryResource(repoSlugOrName))
        {
        }

        internal TagsResource(RepositoryResource repositoryResource)
            : base(repositoryResource, "refs/tags/")
        {
        }

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        public List<Tag> ListTags()
            => ListTags(new ListParameters());

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Tag> ListTags(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return GetPaginatedValues<Tag>(_baseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        public IEnumerable<Tag> EnumerateTags()
            => EnumerateTags(new EnumerateParameters());

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Tag> EnumerateTags(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<Tag>(_baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(CancellationToken token = default)
            => EnumerateTagsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Tag>(_baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif
    }
}
