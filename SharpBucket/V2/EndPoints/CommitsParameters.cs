using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Base class that provide query parameters supported to GET the commits resource.
    /// </summary>
    public abstract class CommitsParameters : ParametersBase
    {
        /// <summary>
        /// An optional path parameter that will limit the results to commits that affect that path.
        /// path can either be a file or a directory.
        /// If a directory is specified, commits are returned that have modified any file in the
        /// directory tree rooted by path.
        /// It is important to note that if the path parameter is specified, the commits returned
        /// by this endpoint may no longer be a DAG, parent commits that do not modify the path
        /// will be omitted from the response.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Names of the references (branch, tag, sha1) that should contains the commits to return.
        /// </summary>
        public ICollection<string> Includes { get; } = new List<string>();

        /// <summary>
        /// Names of the references (branch, tag, sha1) that should not contain the commits to
        /// return.
        /// </summary>
        public ICollection<string> Excludes { get; } = new List<string>();

        internal override IDictionary<string, object> ToDictionary()
        {
            return DictionaryFromKvps(
                KvpOrNull(!string.IsNullOrWhiteSpace(Path), "path", Path),
                KvpOrNull(Includes.Count > 0, "include", Includes),
                KvpOrNull(Excludes.Count > 0, "exclude", Excludes)
            );
        }
    }
}
