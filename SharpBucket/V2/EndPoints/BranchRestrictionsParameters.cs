using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Base class that provide query parameters supported to GET the branch-restrictions resource.
    /// </summary>
    public abstract class BranchRestrictionsParameters : ParametersBase
    {
        /// <summary>
        /// Branch restrictions of this type.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// Branch restrictions applied to branches of this pattern.
        /// </summary>
        public string Pattern { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            return DictionaryFromKvps(
                KvpOrNull(!string.IsNullOrWhiteSpace(Kind), "kind", Kind),
                KvpOrNull(!string.IsNullOrWhiteSpace(Pattern), "pattern", Pattern)
            );
        }
    }
}
