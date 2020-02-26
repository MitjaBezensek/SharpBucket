using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SharpBucket.Utility;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// https://developer.atlassian.com/bitbucket/api/2/reference/resource/repositories/%7Busername%7D/%7Brepo_slug%7D/src#get
    /// </summary>
    public class SrcResource
    {
        private RepositoriesEndPoint RepositoriesEndPoint { get; }
        private Lazy<string> SrcPath { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="SrcResource"/>.
        /// </summary>
        /// <remarks>
        /// If revision is null a non async request will occurs.
        /// if you want a fullly async experience, you should do yourseulf an explicit call to <see cref="RepositoryResource.GetMainBranchRevisionAsync(CancellationToken)"/>
        /// and then provide the result in the <paramref name="revision"/> parameter.
        /// </remarks>
        /// <param name="repositoriesEndPoint">The base end point extended by this resource.</param>
        /// <param name="accountName">The name of the account or team in which is located the repository we want to browse.</param>
        /// <param name="repoSlugOrName">The slug or name of the repository we want to browse. (this may also be the repository UUID).</param>
        /// <param name="revision">The name of the revision to browse. This may be a commit hash, a branch name, a tag name, or null to target the last commit of the main branch.</param>
        /// <param name="path">An optional path to a sub directory if you want to start to browse somewhere else that at the root path.</param>
        public SrcResource(RepositoriesEndPoint repositoriesEndPoint, string accountName, string repoSlugOrName, string revision = null, string path = null)
        {
            RepositoriesEndPoint = repositoriesEndPoint ?? throw new ArgumentNullException(nameof(repositoriesEndPoint));

            var repoPath = $"{accountName.GuidOrValue()}/{repoSlugOrName.ToSlug()}";

            // full build of the SrcPath value is delayed so that when revision is null errors are send
            // only when caller really try to do a request and not when building the resource object
            string BuildSrcPath()
            {
                if (string.IsNullOrEmpty(revision))
                {
                    revision = repositoriesEndPoint.RepositoryResource(accountName, repoSlugOrName).GetMainBranchRevision();
                }

                return UrlHelper.ConcatPathSegments($"{repoPath}/src/", revision, path).EnsureEndsWith('/');
            }

            SrcPath = new Lazy<string>(BuildSrcPath);
        }

        private SrcResource(RepositoriesEndPoint repositoriesEndPoint, string srcPath, string subDirPath)
        {
            RepositoriesEndPoint = repositoriesEndPoint;
            SrcPath = new Lazy<string>(() => UrlHelper.ConcatPathSegments(srcPath, subDirPath).EnsureEndsWith('/'));
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
            return RepositoriesEndPoint.ListTreeEntries(SrcPath.Value, subDirPath, listParameters);
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
            return RepositoriesEndPoint.GetTreeEntry(SrcPath.Value, subPath);
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
        public async Task<TreeEntry> GetTreeEntryAsync(string subPath, CancellationToken token = default)
        {
            return await RepositoriesEndPoint.GetTreeEntryAsync(SrcPath.Value, subPath, token);
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
            return new SrcResource(RepositoriesEndPoint, SrcPath.Value, subDirPath);
        }

        /// <summary>
        /// Gets the raw content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to a file relative to the root of this resource.</param>
        public string GetFileContent(string filePath)
        {
            return RepositoriesEndPoint.GetFileContent(SrcPath.Value, filePath);
        }

        /// <summary>
        /// Gets the raw content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to a file relative to the root of this resource.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async Task<string> GetFileContentAsync(string filePath, CancellationToken token = default)
        {
            return await RepositoriesEndPoint.GetFileContentAsync(SrcPath.Value, filePath, token);
        }
    }
}
