using System;
using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed into methods which return a list of objects
    /// and which support filtering and sorting.
    /// See https://developer.atlassian.com/bitbucket/api/2/reference/meta/filtering for syntax.
    /// </summary>
    public class ListParameters
    {
        /// <summary>
        /// The filter string to apply to the list query.
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// The name of a single field to sort the list of items by.
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// The maximum number of items to return. 0 returns all items.
        /// </summary>
        public int Max { get; set; }

        internal IDictionary<string, object> ToDictionary()
        {
            IDictionary<string, object> result = null;
            
            void AddIfNeeded(string key, string value)
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    result = result ?? (result = new Dictionary<string, object>());
                    result[key] = value;
                }
            }

            AddIfNeeded("q", Filter);
            AddIfNeeded("sort", Sort);

            return result;
        }
    }
}
