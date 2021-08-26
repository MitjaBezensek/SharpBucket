using System.Collections.Generic;
using System.Threading;

using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/workspaces/%7Bworkspace%7D/search/code
    /// </summary>
    public class SearchCodeResource : EndPoint
    {
        internal SearchCodeResource(WorkspaceResource workspaceResource)
            : base(workspaceResource, "search/code")
        {
        }

        /// <summary>
        /// Returns a list of search code results from a search query executed
        /// against all the repositories of the current workspace.
        /// </summary>
        /// <param name="searchQuery">The string that is passed as search query.</param>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        public List<SearchCodeSearchResult> ListSearchResults(string searchQuery, int max = 0)
        {
            var requestParameters = new Dictionary<string, object>
            {
                { "search_query", searchQuery },
            };
            return _sharpBucketV2.GetPaginatedValues<SearchCodeSearchResult>(_baseUrl.TrimEnd('/'), max, requestParameters);
        }

        /// <summary>
        /// Enumerate search code results from a search query executed
        /// against all the repositories of the current workspace.
        /// </summary>
        /// <param name="searchQuery">The string that is passed as search query.</param>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <returns>A lazy enumerable that will request results pages by pages while enumerating the results.</returns>
        public IEnumerable<SearchCodeSearchResult> EnumerateSearchResults(
            string searchQuery,
            int? pageLen = null)
        {
            var requestParameters = new Dictionary<string, object>
            {
                { "search_query", searchQuery },
            };
            return _sharpBucketV2.EnumeratePaginatedValues<SearchCodeSearchResult>(_baseUrl.TrimEnd('/'), requestParameters, pageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate search code results from a search query executed
        /// against all the repositories of the current workspace.
        /// </summary>
        /// <param name="searchQuery">The string that is passed as search query.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>A lazy enumerable that will request results pages by pages while enumerating the results.</returns>
        public IAsyncEnumerable<SearchCodeSearchResult> EnumerateSearchResultsAsync(
            string searchQuery,
            CancellationToken token = default)
            => EnumerateSearchResultsAsync(searchQuery, null, token);

        /// <summary>
        /// Enumerate search code results from a search query executed
        /// against all the repositories of the current workspace.
        /// </summary>
        /// <param name="searchQuery">The string that is passed as search query.</param>
        /// <param name="pageLen">The length of a page. If not defined the default page length will be used.</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>A lazy enumerable that will request results pages by pages while enumerating the results.</returns>
        public IAsyncEnumerable<SearchCodeSearchResult> EnumerateSearchResultsAsync(
            string searchQuery,
            int? pageLen,
            CancellationToken token = default)
        {
            var requestParameters = new Dictionary<string, object>
            {
                { "search_query", searchQuery },
            };
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<SearchCodeSearchResult>(
                _baseUrl.TrimEnd('/'), requestParameters, pageLen, token);
        }
#endif
    }
}
