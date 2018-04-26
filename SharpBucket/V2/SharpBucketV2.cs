using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    /// <summary>
    /// A client for the V2 of the BitBucket API.
    /// You can read more about the V2 of the API here:
    /// https://confluence.atlassian.com/display/BITBUCKET/Version+2
    /// </summary>
    public sealed class SharpBucketV2 : SharpBucket
    {
        internal const string BITBUCKET_URL = "https://api.bitbucket.org/2.0";

        public SharpBucketV2()
        {
            _baseUrl = BITBUCKET_URL;
        }

        public SharpBucketV2(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        /// <summary>
        /// Get the Teams End Point for a specific team.
        /// </summary>
        /// <param name="teamName">The team whose team End Point you wish to get.</param>
        /// <returns></returns>
        public TeamsEndPoint TeamsEndPoint(string teamName)
        {
            return new TeamsEndPoint(this, teamName);
        }

        /// <summary>
        /// Get the Repositories End point.
        /// </summary>
        /// <returns></returns>
        public RepositoriesEndPoint RepositoriesEndPoint()
        {
            return new RepositoriesEndPoint(this);
        }

        /// <summary>
        /// Get the UsersEndPoint End Point.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the UsersEndPoint End Point.</param>
        /// <returns></returns>
        public UsersEndpoint UsersEndPoint(string accountName)
        {
            return new UsersEndpoint(accountName, this);
        }

        public UserEndpoint UserEndPoint()
        {
            return new UserEndpoint(this);
        }
    }
}