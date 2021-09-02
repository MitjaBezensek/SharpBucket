using System.Collections.Generic;
using System.ComponentModel;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    public class EnumerateTeamsParameters : ParametersBase
    {
        /// <summary>
        /// Filters the teams based on the authenticated user's role on each team.
        /// <list type="bullet">
        /// <item>
        /// member: returns a list of all the teams which the caller is a member of at least one
        /// team group or repository owned by the team
        /// </item>
        /// <item>
        /// contributor: returns a list of teams which the caller has write access to at least one
        /// repository owned by the team
        /// </item>
        /// <item>
        /// admin: returns a list teams which the caller has team administrator access
        /// </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// owner role is not a supported value to filter the Teams resource, and is known
        /// to raise a BitBucket exception.
        /// </remarks>
        [DefaultValue(Role.Member)]
        public Role Role { get; set; } = Role.Member;

        /// <summary>
        /// The length of a page. If not defined the default page length will be used.
        /// </summary>
        public int? PageLen { get; set; }

        internal override IDictionary<string, object> ToDictionary()
        {
            var role = this.Role.ToString().ToLowerInvariant();
            return DictionaryFromKvps(
                KvpOrNull(true, "role", role)
            );
        }
    }
}
