using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class ListWorkspacesParameters : ListParameters
    {
        /// <summary>
        /// Filters the workspaces based on the authenticated user's role on each workspace.
        /// </summary>
        public WorkspaceRole? Role { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var dictionary = base.ToDictionary();
            dictionary = AddParameterToDictionary(dictionary, "role", this.Role?.ToString().ToLowerInvariant());
            return dictionary;
        }
    }
}
