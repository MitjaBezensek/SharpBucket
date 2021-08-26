using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
    /// </summary>
    public class SrcResource : EndPoint
    {
        private Lazy<string> SrcPath { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SrcResource"/>.
        /// </summary>
        /// <remarks>
        /// If revision is null a non async request will occurs.
        /// if you want a fully async experience, you should do yourself an explicit call to <see cref="RepositoryResource.GetMainBranchRevisionAsync(CancellationToken)"/>
        /// and then provide the result in the <paramref name="revision"/> parameter.
        /// </remarks>
        /// <param name="repositoriesEndPoint">The base end point extended by this resource.</param>
        /// <param name="accountName">The name of the account or team in which is located the repository we want to browse.</param>
        /// <param name="repoSlugOrName">The slug or name of the repository we want to browse. (this may also be the repository UUID).</param>
        /// <param name="revision">The name of the revision to browse. This may be a commit hash, a branch name, a tag name, or null to target the last commit of the main branch.</param>
        /// <param name="path">An optional path to a sub directory if you want to start to browse somewhere else that at the root path.</param>
        [Obsolete("Prefer repositoriesEndPoint.RepositoriesResource(accountName).RepositoryResource(repoSlugOrName).SrcResource")]
        public SrcResource(RepositoriesEndPoint repositoriesEndPoint, string accountName, string repoSlugOrName, string revision = null, string path = null)
            : this(repositoriesEndPoint.RepositoriesResource(accountName).RepositoryResource(repoSlugOrName), revision, path)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="SrcResource"/>.
        /// </summary>
        /// <remarks>
        /// If revision is null a non async request will occurs.
        /// if you want a fully async experience, you should do yourself an explicit call to <see cref="RepositoryResource.GetMainBranchRevisionAsync(CancellationToken)"/>
        /// and then provide the result in the <paramref name="revision"/> parameter.
        /// </remarks>
        /// <param name="repositoryResource">The parent resource extended by this resource.</param>
        /// <param name="revision">The name of the revision to browse. This may be a commit hash, a branch name, a tag name, or null to target the last commit of the main branch.</param>
        /// <param name="path">An optional path to a sub directory if you want to start to browse somewhere else that at the root path.</param>
        internal SrcResource(RepositoryResource repositoryResource, string revision = null, string path = null)
            : base(repositoryResource, "src")
        {
            // full build of the SrcPath value is delayed so that when revision is null errors are send
            // only when caller really try to do a request and not when building the resource object
            string BuildSrcPath()
            {
                if (string.IsNullOrEmpty(revision))
                {
                    revision = repositoryResource.GetMainBranchRevision();
                }

                return UrlHelper.ConcatPathSegments(revision, path).EnsureEndsWith('/');
            }

            SrcPath = new Lazy<string>(BuildSrcPath);
        }

        private SrcResource(SrcResource parentSrcResource, string srcPath, string subDirPath)
            : base(parentSrcResource, srcPath)
        {
            SrcPath = new Lazy<string>(() => subDirPath.EnsureEndsWith('/'));
        }

        /// <summary>
        /// List the tree entries that are present at the root of this resource, or in the specified sub directory.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="ListSrcEntries"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="listParameters">Parameters for the query.</param>
        public List<TreeEntry> ListTreeEntries(string subDirPath = null, ListParameters listParameters = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, subDirPath);
            return listParameters == null
                ? GetPaginatedValues<TreeEntry>(overrideUrl)
                : GetPaginatedValues<TreeEntry>(overrideUrl, listParameters.Max, listParameters.ToDictionary());
        }

        /// <summary>
        /// List the source files and directories that are present at the root of this resource, or in the specified sub directory.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="listParameters">Parameters for the query.</param>
        public List<SrcEntry> ListSrcEntries(string subDirPath = null, ListParameters listParameters = null)
        {
            return ListTreeEntries(subDirPath, listParameters)
                .Select(treeEntry => treeEntry.ToSrcEntry())
                .ToList();
        }

        /// <summary>
        /// Enumerate the tree entries that are present at the root of this resource, or in the specified sub directory.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="EnumerateSrcEntries"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<TreeEntry> EnumerateTreeEntries(string subDirPath = null, EnumerateParameters parameters = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, subDirPath);
            return SharpBucketV2.EnumeratePaginatedValues<TreeEntry>(
                overrideUrl, parameters?.ToDictionary(), parameters?.PageLen);
        }

        /// <summary>
        /// Enumerate the source files and directories that are present at the root of this resource,
        /// or in the specified sub directory.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<SrcEntry> EnumerateSrcEntries(string subDirPath = null, EnumerateParameters parameters = null)
        {
            return EnumerateTreeEntries(subDirPath, parameters)
                .Select(treeEntry => treeEntry.ToSrcEntry());
        }

#if CS_8
        /// <summary>
        /// Enumerate the tree entries that are present at the root of this resource, or in the specified sub directory.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="EnumerateSrcEntriesAsync"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">The cancellation token</param>
        public IAsyncEnumerable<TreeEntry> EnumerateTreeEntriesAsync(
            string subDirPath = null, EnumerateParameters parameters = null, CancellationToken token = default)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, subDirPath);
            return SharpBucketV2.EnumeratePaginatedValuesAsync<TreeEntry>(
                overrideUrl, parameters?.ToDictionary(), parameters?.PageLen, token);
        }

        /// <summary>
        /// Enumerate the source files and directories that are present at the root of this resource,
        /// or in the specified sub directory.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">The cancellation token</param>
        public async IAsyncEnumerable<SrcEntry> EnumerateSrcEntriesAsync(
            string subDirPath = null,
            EnumerateParameters parameters = null,
            [EnumeratorCancellation]CancellationToken token = default)
        {
            await foreach (var treeEntry in EnumerateTreeEntriesAsync(subDirPath, parameters, token))
            {
                yield return treeEntry.ToSrcEntry();
            }
        }
#endif

        /// <summary>
        /// Gets the metadata of a specified sub path in this resource.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="GetSrcEntry"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        public TreeEntry GetTreeEntry(string subPath = null)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, subPath);
            return SharpBucketV2.Get<TreeEntry>(overrideUrl, new { format = "meta" });
        }

        /// <summary>
        /// Gets the metadata of the root of this resource.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="GetSrcEntry"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<TreeEntry> GetTreeEntryAsync(CancellationToken token = default)
        {
            return GetTreeEntryAsync(null, token);
        }

        /// <summary>
        /// Gets the metadata of a specified sub path in this resource.
        /// <remarks>
        /// Since it can be difficult to guess which field is filled or not in a <see cref="TreeEntry"/>,
        /// we suggest you to use <see cref="GetSrcEntry"/> method instead of that one,
        /// except if you really want to retrieve the raw model as returned by BitBucket.
        /// </remarks>
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<TreeEntry> GetTreeEntryAsync(string subPath, CancellationToken token = default)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, subPath);
            return SharpBucketV2.GetAsync<TreeEntry>(overrideUrl, new { format = "meta" }, token);
        }

        /// <summary>
        /// Gets the metadata of a specified file or directory in this resource.
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        public SrcEntry GetSrcEntry(string subPath = null)
        {
            return GetTreeEntry(subPath).ToSrcEntry();
        }

        /// <summary>
        /// Gets the metadata of a specified file or directory in this resource.
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        /// <param name="token">The cancellation token</param>
        public async Task<SrcEntry> GetSrcEntryAsync(string subPath = null, CancellationToken token = default)
        {
            var treeEntry = await GetTreeEntryAsync(subPath, token: token);
            return treeEntry.ToSrcEntry();
        }

        /// <summary>
        /// Gets the metadata of a specified file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        public SrcFile GetSrcFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            return (SrcFile)GetTreeEntry(filePath).ToSrc();
        }

        /// <summary>
        /// Gets the metadata of a specified file.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="token">The cancellation token</param>
        public async Task<SrcFile> GetSrcFileAsync(string filePath, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));
            var treeEntry = await GetTreeEntryAsync(filePath, token: token);
            return (SrcFile)treeEntry.ToSrc();
        }

        /// <summary>
        /// Gets the metadata of a specified directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory, or null to retrieve the metadata of the root of this resource.</param>
        public SrcDirectory GetSrcDirectory(string directoryPath)
        {
            return (SrcDirectory)GetTreeEntry(directoryPath).ToSrc();
        }

        /// <summary>
        /// Gets the metadata of a specified directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory, or null to retrieve the metadata of the root of this resource.</param>
        /// <param name="token">The cancellation token</param>
        public async Task<SrcDirectory> GetSrcDirectoryAsync(string directoryPath, CancellationToken token = default)
        {
            var treeEntry = await GetTreeEntryAsync(directoryPath, token: token);
            return (SrcDirectory)treeEntry.ToSrc();
        }

        /// <summary>
        /// Gets a new <see cref="SrcResource"/> which is rooted on a sub directory of the current <see cref="SrcResource"/>.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory.</param>
        public SrcResource SubSrcResource(string subDirPath)
        {
            if (string.IsNullOrWhiteSpace(subDirPath)) throw new ArgumentNullException(nameof(subDirPath));
            return new SrcResource(this, SrcPath.Value, subDirPath);
        }

        /// <summary>
        /// Gets the raw content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to a file relative to the root of this resource.</param>
        public string GetFileContent(string filePath)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, filePath);
            return SharpBucketV2.Get(overrideUrl);
        }

        /// <summary>
        /// Gets the raw content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to a file relative to the root of this resource.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public Task<string> GetFileContentAsync(string filePath, CancellationToken token = default)
        {
            var overrideUrl = UrlHelper.ConcatPathSegments(BaseUrl, SrcPath.Value, filePath);
            return SharpBucketV2.GetAsync(overrideUrl, token);
        }
    }
}
