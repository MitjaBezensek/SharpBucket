using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Parameters for RepositoryResource.GetDiff().
    /// </summary>
    public class DiffParameters: ParametersBase
    {
        /// <summary>
        /// Number of context lines for each difference; default is 3.
        /// </summary>
        public byte Context { get; set; } = 3;
        /// <summary>
        /// The paths of specific files to diff.
        /// </summary>
        public List<string> Paths { get; set; } = new List<string>();
        /// <summary>
        /// Generate diffs that ignore whitespace; default is false.
        /// </summary>
        public bool IgnoreWhitespace { get; set; }
        /// <summary>
        /// Generate diffs that include binary files; default is true.
        /// </summary>
        public bool Binary { get; set; } = true;

        internal override IDictionary<string, object> ToDictionary()
        {
            return DictionaryFromKvps(
                KvpOrNull(Context != 3, "context", Context),
                KvpOrNull(Paths?.Any() == true, "path", Paths),
                KvpOrNull(IgnoreWhitespace, "ignore_whitespace", "true"),
                KvpOrNull(!Binary, "binary", "false")
            );
        }
    }
}
