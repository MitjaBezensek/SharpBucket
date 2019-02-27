using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                var rootSrcPath = $"{repoPath}/src/";

                if (string.IsNullOrEmpty(revision))
                {
                    try
                    {
                        // This may potentially be optimized since this call will first hit a redirect toward an url that contains the revision
                        // but the actual architecture of the code doesn't allow us to fetch just the redirect location of a GET request.
                        // so we found back the data we need in the response of the call to the url where we are redirected.
                        var rootEntry = repositoriesEndPoint.GetTreeEntry(rootSrcPath);
                        revision = rootEntry.commit.hash;
                    }
                    catch (BitbucketV2Exception e) when (e.HttpStatusCode == HttpStatusCode.NotFound)
                    {
                        // mimic the error that bitbucket send when we perform calls on src endpoint with a revision name
                        var errorResponse = new ErrorResponse {type = "Error", error = new Error {message = $"Repository {repoPath} not found"}};
                        throw new BitbucketV2Exception(HttpStatusCode.NotFound, errorResponse);
                    }
                }

                return UrlHelper.ConcatPathSegments(rootSrcPath, revision, path).EnsureEndsWith('/');
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
        /// Gets the metadata of a specified file or directory in this resource.
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        public SrcEntry GetSrcEntry(string subPath = null)
        {
            return GetTreeEntry(subPath).ToSrcEntry();
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
        /// Gets the metadata of a specified directory.
        /// </summary>
        /// <param name="directoryPath">The path to the directory, or null to retrieve the metadata of the root of this resource.</param>
        public SrcDirectory GetSrcDirectory(string directoryPath)
        {
            return (SrcDirectory)GetTreeEntry(directoryPath).ToSrc();
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
    }
}
