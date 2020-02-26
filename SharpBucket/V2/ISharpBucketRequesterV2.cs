using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    /// <summary>
    /// Interface that expose all the methods that an end point V2 could use to perform calls to the BitBucket API.
    /// </summary>
    public interface ISharpBucketRequesterV2 : ISharpBucketRequester
    {
        /// <summary>
        /// Get the Teams end pPoint.
        ///  Give access to the data relative to the teams.
        /// </summary>
        TeamsEndPoint TeamsEndPoint();

        /// <summary>
        /// Get the Repositories end point.
        /// Give access to the data relative to the public repositories and private repositories of the logged in user.
        /// </summary>
        RepositoriesEndPoint RepositoriesEndPoint();

        /// <summary>
        /// Get the Users end point.
        /// Give access to the data relative to a specified user.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the end point.</param>
        UsersEndpoint UsersEndPoint(string accountName);

        /// <summary>
        /// Get the User end point.
        /// Give access to the data relative to the currently logged in user.
        /// </summary>
        UserEndpoint UserEndPoint();
    }
}