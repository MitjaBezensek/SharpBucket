using System;
using System.Collections.Generic;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// The Teams End Point gets a team's profile information.
    /// More info:
    /// https://confluence.atlassian.com/display/BITBUCKET/teams+Endpoint
    /// </summary>
    public class TeamsEndPoint : EndPoint
    {
        public TeamsEndPoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "teams/")
        {
        }

        /// <summary>
        /// Returns a list of all the teams which the caller is a member of at least one team group or repository owned by the team
        /// </summary>
        public List<Team> GetUserTeams(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "member" } };
            return GetPaginatedValues<Team>(_baseUrl, max, parameters);
        }

        /// <summary>
        /// Returns a list of teams which the caller has write access to at least one repository owned by the team.
        /// </summary>
        public List<Team> GetUserTeamsWithContributorRole(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "contributor" } };
            return GetPaginatedValues<Team>(_baseUrl, max, parameters);
        }

        /// <summary>
        /// Returns a list teams which the caller has team administrator access.
        /// </summary>
        public List<Team> GetUserTeamsWithAdminRole(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "admin" } };
            return GetPaginatedValues<Team>(_baseUrl, max, parameters);
        }

        /// <summary>
        /// Gets a <see cref="TeamResource"/> for a specified team.
        /// </summary>
        /// <param name="teamName">The team's username or UUID.</param>
        public TeamResource TeamResource(string teamName)
        {
            return new TeamResource(this._sharpBucketV2, teamName);
        }
    }
}