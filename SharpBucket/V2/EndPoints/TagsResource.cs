using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            : base(repositoryResource, "refs/tags")
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
            return GetPaginatedValues<Tag>(BaseUrl, parameters.Max, parameters.ToDictionary());
        }

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing requests page by page while enumerating.
        /// </summary>
        public IEnumerable<Tag> EnumerateTags()
            => EnumerateTags(new EnumerateParameters());

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Tag> EnumerateTags(EnumerateParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValues<Tag>(BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(CancellationToken token = default)
            => EnumerateTagsAsync(new EnumerateParameters(), token);

        /// <summary>
        /// Enumerate all Tags associated with a specific repository,
        /// doing requests page by page while enumerating.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<Tag> EnumerateTagsAsync(EnumerateParameters parameters, CancellationToken token = default)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Tag>(BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>A new tag instance that fully represent the newly created tag.</returns>
        public Tag PostTag(Tag tag)
        {
            return SharpBucketV2.Post(tag, BaseUrl);
        }

        /// <summary>
        /// Create a new tag.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A new tag instance that fully represent the newly created tag.</returns>
        public async Task<Tag> PostTagAsync(Tag tag, CancellationToken token = default)
        {
            return await SharpBucketV2.PostAsync(tag, BaseUrl, token);
        }

        /// <summary>
        /// Get a tag.
        /// </summary>
        /// <param name="name">The name of the tag</param>
        public Tag GetTag(string name)
        {
            return SharpBucketV2.Get<Tag>(BaseUrl + "/" + name);
        }

        /// <summary>
        /// Get a tag.
        /// </summary>
        /// <param name="name">The name of the tag</param>
        /// <param name="token">The cancellation token</param>
        public Task<Tag> GetTagAsync(string name, CancellationToken token = default)
        {
            return SharpBucketV2.GetAsync<Tag>(BaseUrl + "/" + name, token);
        }

        /// <summary>
        /// Delete a tag.
        /// </summary>
        /// <param name="name">The name of the tag</param>
        public void DeleteTag(string name)
        {
            SharpBucketV2.Delete(BaseUrl + "/" + name);
        }

        /// <summary>
        /// Delete a tag.
        /// </summary>
        /// <param name="name">The name of the tag</param>
        /// <param name="token">The cancellation token</param>
        public Task DeleteTagAsync(string name, CancellationToken token = default)
        {
            return SharpBucketV2.DeleteAsync(BaseUrl + "/" + name, token);
        }
    }
}
