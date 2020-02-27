using System;
using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that enumerate public repositories.
    /// </summary>
    public class EnumeratePublicRepositoriesParameters : EnumerateRepositoriesParameters
    {
        /// <summary>
        /// Filter the results to include only repositories created on or after this date.
        /// </summary>
        public DateTime? After { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = ListPublicRepositoriesParameters.AddAfter(dictionary, this.After);
            return dictionary;
        }
    }
}
