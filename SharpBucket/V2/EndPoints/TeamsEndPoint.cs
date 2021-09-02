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
    [Obsolete("This endpoint has been deprecated and will stop functioning soon. You should use the WorkspacesEndPoint instead.")]
    public class TeamsEndPoint : EndPoint
    {
        [Obsolete("Use WorkspacesEndPoint(ISharpBucketRequesterV2) instead.")]
        public TeamsEndPoint(ISharpBucketRequesterV2 sharpBucketV2)
            : base(sharpBucketV2, "teams")
        {
        }

        /// <summary>
        /// Returns a list of all the teams which the caller is a member of at least one team group or repository owned by the team
        /// </summary>
        [Obsolete("Use WorkspacesEndPoint.ListWorkspaces(new ListWorkspacesParameters { Role = WorkspaceRole.Member }) instead.")]
        public List<Team> GetUserTeams(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "member" } };
            return GetPaginatedValues<Team>(BaseUrl, max, parameters);
        }

        /// <summary>
        /// Returns a list of teams which the caller has write access to at least one repository owned by the team.
        /// </summary>
        [Obsolete("Use WorkspacesEndPoint.ListWorkspaces(new ListWorkspacesParameters { Role = WorkspaceRole.Collaborator }) instead.")]
        public List<Team> GetUserTeamsWithContributorRole(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "contributor" } };
            return GetPaginatedValues<Team>(BaseUrl, max, parameters);
        }

        /// <summary>
        /// Returns a list teams which the caller has team administrator access.
        /// </summary>
        [Obsolete("Use WorkspacesEndPoint.ListWorkspaces(new ListWorkspacesParameters { Role = WorkspaceRole.Owner }) instead.")]
        public List<Team> GetUserTeamsWithAdminRole(int max = 0)
        {
            var parameters = new Dictionary<string, object> { { "role", "admin" } };
            return GetPaginatedValues<Team>(BaseUrl, max, parameters);
        }

        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        [Obsolete("Use WorkspacesEndPoint.EnumerateWorkspaces() instead.")]
        public IEnumerable<Team> EnumerateUserTeams()
            => EnumerateUserTeams(new EnumerateTeamsParameters());

        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        [Obsolete("Use WorkspacesEndPoint.EnumerateWorkspaces(EnumerateWorkspacesParameters) instead.")]
        public IEnumerable<Team> EnumerateUserTeams(EnumerateTeamsParameters parameters)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValues<Team>(
                BaseUrl, parameters.ToDictionary(), parameters.PageLen);
        }

#if CS_8
        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        [Obsolete("Use WorkspacesEndPoint.EnumerateWorkspacesAsync() instead.")]
        public IAsyncEnumerable<Team> EnumerateUserTeamsAsync(CancellationToken token = default)
            => EnumerateUserTeamsAsync(new EnumerateTeamsParameters(), token);

        /// <summary>
        /// Enumerate all the teams that the authenticated user is associated with.
        /// </summary>
        /// <param name="parameters">Parameters for the query.</param>
        /// <param name="token">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        [Obsolete("Use WorkspacesEndPoint.EnumerateWorkspacesAsync(EnumerateWorkspacesParameters) instead.")]
        public IAsyncEnumerable<Team> EnumerateUserTeamsAsync(
            EnumerateTeamsParameters parameters, CancellationToken token = default)
        {
            _ = parameters ?? throw new ArgumentNullException(nameof(parameters));
            return SharpBucketV2.EnumeratePaginatedValuesAsync<Team>(
                BaseUrl, parameters.ToDictionary(), parameters.PageLen, token);
        }
#endif

        /// <summary>
        /// Gets a <see cref="TeamResource"/> for a specified team.
        /// </summary>
        /// <param name="teamName">The team's username or UUID.</param>
        [Obsolete("Use WorkspacesEndPoint.WorkspaceResource(string) instead.")]
        public TeamResource TeamResource(string teamName)
        {
            return new TeamResource(this, teamName);
        }
    }
}