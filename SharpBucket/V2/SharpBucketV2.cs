using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    /// <summary>
    /// A client for the V2 of the BitBucket API.
    /// You can read more about the V2 of the API here:
    /// https://confluence.atlassian.com/display/BITBUCKET/Version+2
    /// </summary>
    public sealed class SharpBucketV2 : SharpBucket, ISharpBucketV2
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

        /// <inheritdoc />
        public TeamsEndPoint TeamsEndPoint(string teamName)
        {
            return new TeamsEndPoint(this, teamName);
        }

        /// <inheritdoc />
        public RepositoriesEndPoint RepositoriesEndPoint()
        {
            return new RepositoriesEndPoint(this);
        }

        /// <inheritdoc />
        public UsersEndpoint UsersEndPoint(string accountName)
        {
            return new UsersEndpoint(accountName, this);
        }

        /// <inheritdoc />
        public UserEndpoint UserEndPoint()
        {
            return new UserEndpoint(this);
        }
    }
}