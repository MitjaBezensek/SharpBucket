using System;
using System.Collections.Generic;
using System.Text;
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
        private string SrcPath { get; }

        public SrcResource(RepositoriesEndPoint repositoriesEndPoint, string accountName, string repoSlugOrName, string revision = null, string path = null)
        {
            RepositoriesEndPoint = repositoriesEndPoint ?? throw new ArgumentNullException(nameof(repositoriesEndPoint));
            SrcPath = ConcatPathSegments($"{accountName.GuidOrValue()}/{repoSlugOrName.ToSlug()}/src", revision, path);
        }

        private SrcResource(RepositoriesEndPoint repositoriesEndPoint, string srcPath, string subDirPath)
        {
            RepositoriesEndPoint = repositoriesEndPoint;
            SrcPath = ConcatPathSegments(srcPath, subDirPath);
        }

        /// <summary>
        /// List the files and directories that are present at the root of this resource, or in the specified sub directory.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory, or null to list the root directory of this resource.</param>
        /// <param name="max">The maximum number of entries to return, or 0 for unlimited size.</param>
        public List<TreeEntry> ListTreeEntries(string subDirPath = null, int max = 0)
        {
            return RepositoriesEndPoint.ListTreeEntries(SrcPath, subDirPath, max);
        }

        /// <summary>
        /// Gets the metadata of a specified file or directory in this resource.
        /// </summary>
        /// <param name="subPath">The path to the file or directory, or null to retrieve the metadata of the root of this resource.</param>
        public TreeEntry GetTreeEntry(string subPath = null)
        {
            return RepositoriesEndPoint.GetTreeEntry(SrcPath, subPath);
        }

        /// <summary>
        /// Gets a new <see cref="SrcResource"/> which is rooted on a sub directory of the current <see cref="SrcResource"/>.
        /// </summary>
        /// <param name="subDirPath">The path to a sub directory.</param>
        public SrcResource SubSrcResource(string subDirPath)
        {
            if (string.IsNullOrWhiteSpace(subDirPath)) throw new ArgumentNullException(nameof(subDirPath));
            return new SrcResource(RepositoriesEndPoint, SrcPath, subDirPath);
        }

        /// <summary>
        /// Gets the raw content of the specified file.
        /// </summary>
        /// <param name="filePath">The path to a file relative to the root of this resource.</param>
        internal string GetFileContent(string filePath)
        {
            return RepositoriesEndPoint.GetFileContent(SrcPath, filePath);
        }

        private static string ConcatPathSegments(params string[] pathSegments)
        {
            var path = new StringBuilder();
            foreach (var pathSegment in pathSegments)
            {
                if (string.IsNullOrEmpty(pathSegment)) return path.ToString();
                path.Append(pathSegment);
                path.Append("/");
            }
            return path.ToString();
        }
    }
}
