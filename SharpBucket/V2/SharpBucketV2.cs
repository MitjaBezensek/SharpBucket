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
        /// </summary>
        /// <param name="teamName">The team whose team End Point you wish to get.</param>
        /// <returns></returns>
        public TeamsEndPoint Teams(string teamName){
            return new TeamsEndPoint(this, teamName);
        }

        /// <summary>
        /// Get the Repositories End point.
        /// </summary>
        /// <returns></returns>
        public RepositoriesEndPoint Repositories(){
            return new RepositoriesEndPoint(this);
        }

        /// <summary>
        /// Get the Users End Point.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the Users End Point.</param>
        /// <returns></returns>
        public UsersEndpoint Users(string accountName){
            return new UsersEndpoint(accountName, this);
        }
    }
}