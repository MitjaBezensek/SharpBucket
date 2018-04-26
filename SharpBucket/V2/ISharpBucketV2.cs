using SharpBucket.Authentication;
using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    public interface ISharpBucketV2 : ISharpBucket
    {
        /// <summary>
        /// Get the Teams End Point for a specific team.
        /// </summary>
        /// <param name="teamName">The team whose team End Point you wish to get.</param>
        /// <returns></returns>
        TeamsEndPoint TeamsEndPoint(string teamName);

        /// <summary>
        /// Get the Repositories End point.
        /// </summary>
        /// <returns></returns>
        RepositoriesEndPoint RepositoriesEndPoint();

        /// <summary>
        /// Get the UsersEndPoint End Point.
        /// </summary>
        /// <param name="accountName">The account for which you wish to get the UsersEndPoint End Point.</param>
        /// <returns></returns>
        UsersEndpoint UsersEndPoint(string accountName);

        /// <summary>
        /// Get the UserEndpoint End Point
        /// </summary>
        /// <returns></returns>
        UserEndpoint UserEndPoint();
    }
}