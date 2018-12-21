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
        /// Path of a particular file to diff.
        /// </summary>
        public string Path { get; set; }
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
            var parameters = new []
            {
                new Parameter(Context != 3, "context", Context),
                new Parameter(!String.IsNullOrWhiteSpace(Path), "path", Path),
                new Parameter(IgnoreWhitespace, "ignore_whitespace", "true"),
                new Parameter(!Binary, "binary", "false")
            };

            return DictionaryFromParameters(parameters);
        }
    }
}
