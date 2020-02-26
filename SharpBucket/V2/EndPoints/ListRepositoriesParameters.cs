using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that list repositories.
    /// </summary>
    public class ListRepositoriesParameters : ListParameters
    {
        /// <summary>
        /// Filters the result based on the authenticated user's role on each repository.
        /// </summary>
        public Role? Role { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = AddRole(dictionary, this.Role);
            return dictionary;
        }

        internal static IDictionary<string, object> AddRole(IDictionary<string, object> dictionary, Role? role)
        {
            if (role != null)
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<string, object>();
                }
                dictionary.Add("role", role.Value.ToString().ToLowerInvariant());
            }

            return dictionary;
        }
    }
}
