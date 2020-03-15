using System;
using System.Collections.Generic;
using System.Threading;
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
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        public IEnumerable<Team> EnumerateUserTeams()
            => EnumerateUserTeams(new EnumerateTeamsParameters());

        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IEnumerable<Team> EnumerateUserTeams(EnumerateTeamsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValues<Team>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        public IAsyncEnumerable<Team> EnumerateUserTeamsAsync(CancellationToken token = default)
            => EnumerateUserTeamsAsync(new EnumerateTeamsParameters(), token);

        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        public IAsyncEnumerable<Team> EnumerateUserTeamsAsync(
            EnumerateTeamsParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return _sharpBucketV2.EnumeratePaginatedValuesAsync<Team>(
                _baseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

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