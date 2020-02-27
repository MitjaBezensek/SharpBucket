using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed into methods which enumerate queryable objects
    /// and which support filtering and sorting.
    /// See https://developer.atlassian.com/bitbucket/api/2/reference/meta/filtering for syntax.
    /// </summary>
    public class EnumerateParameters : ParametersBase
    {
        /// <summary>
        /// The filter string to apply to the query.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// The name of a single field to sort the items.
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// The length of a page. If not defined the default page length will be used.
        /// </summary>
        public int? PageLen { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            return DictionaryFromKvps(
                KvpOrNull(!string.IsNullOrWhiteSpace(Filter), "q", Filter),
                KvpOrNull(!string.IsNullOrWhiteSpace(Sort), "sort", Sort)
            );        
        }
    }
}
