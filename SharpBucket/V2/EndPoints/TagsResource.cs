using System;
using System.Collections.Generic;
using System.Threading;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Manage tags for a repository.
    /// More info:
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Bworkspace%7D/%7Brepo_slug%7D/refs/tags
    /// </summary>
    public class TagsResource
    {
        private readonly string _accountName;
        private readonly string _slug;
        private readonly RepositoriesEndPoint _repositoriesEndPoint;

        public TagsResource(string accountName, string repoSlugOrName, RepositoriesEndPoint repositoriesEndPoint)
        {
            _accountName = accountName.GuidOrValue();
            _slug = repoSlugOrName.ToSlug();
            _repositoriesEndPoint = repositoriesEndPoint;
        }

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        public List<Tag> ListTags() => ListTags(new ListParameters());

        /// <summary>
        /// Lists all Tags associated with a specific repository.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public List<Tag> ListTags(ListParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.ListTags(_accountName, _slug, parameters);
        }

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        public IEnumerable<Tag> EnumerateTags() => EnumerateTags(new EnumerateParameters());

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Tag> EnumerateTags(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.EnumerateTags(_accountName, _slug, parameters);
        }

#if CS_8
        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(CancellationToken token = default)
            => EnumerateTagsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing reqests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return _repositoriesEndPoint.EnumerateTagsAsync(_accountName, _slug, parameters, token);
        }
#endif
    }
}
