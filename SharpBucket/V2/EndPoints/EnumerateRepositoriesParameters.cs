using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that enumerate repositories.
    /// </summary>
    public class EnumerateRepositoriesParameters : EnumerateParameters
    {
        /// <summary>
        /// Filters the result based on the authenticated user's role on each repository.
        /// </summary>
        public Role? Role { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = AddParameterToDictionary(dictionary, "role", this.Role?.ToString().ToLowerInvariant());
            return dictionary;
        }
    }
}
