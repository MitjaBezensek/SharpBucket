using System;
using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that list public repositories.
    /// </summary>
    public class ListPublicRepositoriesParameters : ListRepositoriesParameters
    {
        /// <summary>
        /// Filter the results to include only repositories created on or after this date.
        /// </summary>
        public DateTime? After { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = AddAfter(dictionary, this.After);
            return dictionary;
        }

        internal static IDictionary<string, object> AddAfter(IDictionary<string, object> dictionary, DateTime? after)
        {
            if (after != null)
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, object>();
                }
                dictionary.Add("after", after.Value.ToString("o"));
            }

            return dictionary;
        }
    }
}
