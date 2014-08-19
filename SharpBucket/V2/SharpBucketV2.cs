using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2{
    /// <summary>
    /// A client for the V2 of the BitBucket API.
    /// You can read more about the V2 of the API here:
    /// https://confluence.atlassian.com/display/BITBUCKET/Version+2
    /// </summary>
    public sealed class SharpBucketV2 : SharpBucket{
        public SharpBucketV2(){
            _baseUrl = "https://bitbucket.org/api/2.0/";
        }

        /// <summary>
        /// Get the Teams End Point for a specific team.
        /// The Teams End Point is used for getting the information about the team, like:
        /// team profile, team members,...
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/teams+Endpoint
        /// </summary>
        /// <param name="teamName">The team whose end point you wish to get.</param>
        /// <returns></returns>
        public TeamsEndPointV2 Teams(string teamName){
            return new TeamsEndPointV2(this, teamName);
        }

        /// <summary>
        /// Get the Repositories End point.
        /// The Repositories End Point is used for getting the information about:
        /// public repositories, repositories for a specific account.
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/repositories+Endpoint
        /// </summary>
        /// <returns></returns>
        public RepositoriesEndPointV2 Repositories(){
            return new RepositoriesEndPointV2(this);
        }

        /// <summary>
        /// Get the Users End Point.
        /// The Users End Point is used for getting the information about:
        /// the users profile, the list of users followers,...
        /// More info here:
        /// https://confluence.atlassian.com/display/BITBUCKET/users+Endpoint
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public UsersEndpointV2 Users(string accountName){
            return new UsersEndpointV2(accountName, this);
        }
    }
}