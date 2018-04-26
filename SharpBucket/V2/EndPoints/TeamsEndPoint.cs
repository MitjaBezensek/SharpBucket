using System.Collections.Generic;
using System.Dynamic;
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
        public TeamsEndPoint(SharpBucketV2 sharpBucketV2, string teamName)
            : base(sharpBucketV2, "teams/" + teamName + "/")
        {
        }
        public TeamsEndPoint(ISharpBucketV2 sharpBucketV2, string teamName)
            : base(sharpBucketV2, "teams/" + teamName + "/")
        {
        }


        public List<Team> GetUserTeams(int max = 0)
        {
            dynamic parameters = new ExpandoObject();
            parameters.role = "member";
            return GetPaginatedValues<Team>("teams/", max, parameters);
            //return _sharpBucketV2.Get<List<Team>>(null, "teams/", parameters);
        }

        /// <summary>
        /// Gets the public information associated with a team. 
        /// If the team's profile is private, the caller must be authenticated and authorized to view this information. 
        /// </summary>
        /// <returns></returns>
        public Team GetProfile()
        {
            return _sharpBucketV2.Get(new Team(), _baseUrl);
        }

        /// <summary>
        /// Gets the team's members.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListMembers(int max = 0)
        {
            var overrideUrl = _baseUrl + "members/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of accounts following the team.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowers(int max = 0)
        {
            var overrideUrl = _baseUrl + "followers/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets a list of accounts the team is following.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Team> ListFollowing(int max = 0)
        {
            var overrideUrl = _baseUrl + "following/";
            return GetPaginatedValues<Team>(overrideUrl, max);
        }

        /// <summary>
        /// Gets the list of the team's repositories. 
        /// Private repositories only appear on this list if the caller is authenticated and is authorized to view the repository.
        /// </summary>
        /// <param name="max">The maximum number of items to return. 0 returns all items.</param>
        /// <returns></returns>
        public List<Repository> ListRepositories(int max = 0)
        {
            var overrideUrl = _baseUrl + "repositories/";
            return GetPaginatedValues<Repository>(overrideUrl, max);
        }
    }
}