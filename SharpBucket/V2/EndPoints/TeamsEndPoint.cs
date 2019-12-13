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
        private readonly TeamResource _teamResource;

        public TeamsEndPoint(SharpBucketV2 sharpBucketV2)
            : base(sharpBucketV2, "teams/")
        {
        }

        [Obsolete("Use TeamResource class to manipulate a team")]
        public TeamsEndPoint(SharpBucketV2 sharpBucketV2, string teamName)
            : this(sharpBucketV2)
        {
            // initially when teamName was null there where no check in the constructor
            // which means that only the methods that need it will fails and methods that doesn't works
            // this test is here to reproduce that legacy behaviour until we delete the obsolete code
            if (!string.IsNullOrEmpty(teamName))
            {
                _teamResource = new TeamResource(sharpBucketV2, teamName);
            }
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

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use TeamResource.GetProfile() instead")]
        public Team GetProfile()
        {
            if (_teamResource == null) throw new InvalidOperationException("This method could be used only with obsolete constructor, when a team name has been provided");
            return _teamResource.GetProfile();
        }

        /// <summary>
        /// Gets the team's members.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("Use TeamResource.ListMembers() instead")]
        public List<Team> ListMembers(int max = 0)
        {
            if (_teamResource == null) throw new InvalidOperationException("This method could be used only with obsolete constructor, when a team name has been provided");
            return _teamResource.ListMembers(max);
        }

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("Use TeamResource.ListFollowers() instead")]
        public List<Team> ListFollowers(int max = 0)
        {
            if (_teamResource == null) throw new InvalidOperationException("This method could be used only with obsolete constructor, when a team name has been provided");
            return _teamResource.ListFollowers(max);
        }

        /// <summary>
        /// Gets a list of accounts the team is following.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("Use TeamResource.ListFollowing() instead")]
        public List<Team> ListFollowing(int max = 0)
        {
            if (_teamResource == null) throw new InvalidOperationException("This method could be used only with obsolete constructor, when a team name has been provided");
            return _teamResource.ListFollowing(max);
        }

        /// <summary>
        /// Gets the list of the team's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        [Obsolete("Use TeamResource.ListRepositories() instead")]
        public List<Repository> ListRepositories(int max = 0)
        {
            if (_teamResource == null) throw new InvalidOperationException("This method could be used only with obsolete constructor, when a team name has been provided");
            return _teamResource.ListRepositories(new ListParameters { Max = max });
        }
    }
}