using System;
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
            : this(BITBUCKET_URL)
        {
        }

        public SharpBucketV2(string baseUrl)
            :base(baseUrl, new RequestExecutorV2())
        {
        }

        /// <summary>
        /// Get the Teams endpoint.
        ///  Give access to the data relative to the teams.
        /// </summary>
        [Obsolete("This endpoint has been deprecated and will stop functioning soon. You should use WorkspacesEndPoint() instead.")]
        public TeamsEndPoint TeamsEndPoint()
        {
            return new TeamsEndPoint(this);
        }

        /// <summary>
        /// Get the Repositories endpoint.
        /// Give access to the data relative to the public repositories and private repositories of the logged in user.
        /// </summary>
        public RepositoriesEndPoint RepositoriesEndPoint()
        {
            return new RepositoriesEndPoint(this);
        }

        /// <summary>
        /// Get the Users endpoint.
        /// Give access to the data relative to a specified user.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the end point.</param>
        public UsersEndpoint UsersEndPoint(string accountName)
        {
            return new UsersEndpoint(accountName, this);
        }

        /// <summary>
        /// Get the User endpoint.
        /// Give access to the data relative to the currently logged in user.
        /// </summary>
        public UserEndpoint UserEndPoint()
        {
            return new UserEndpoint(this);
        }

        /// <summary>
        /// Get the Workspaces endpoint.
        /// </summary>
        public WorkspacesEndPoint WorkspacesEndPoint()
        {
            return new WorkspacesEndPoint(this);
        }
    }
}