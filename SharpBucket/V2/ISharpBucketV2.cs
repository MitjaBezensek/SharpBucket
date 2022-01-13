using System;
using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2
{
    /// <summary>
    /// Interface that expose all the methods available on the <see cref="SharpBucketV2"/> class.
    /// This interface should be used for mocking <see cref="SharpBucketV2"/>.
    /// </summary>
    /// <remarks>
    /// Since this interface may change in the future (typically to declare new endpoints),
    /// We recommend to not create wrappers in our code and limit usages to mocking scenarios.
    /// If you want to create wrappers or similar things, it's preferred to just do it over the
    /// <see cref="ISharpBucketRequesterV2"/> interface which is expected to be more stable in future developments.
    /// </remarks>
    public interface ISharpBucketV2 : ISharpBucket, ISharpBucketRequesterV2
    {
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

        /// <summary>
        /// Get the Workspaces endpoint.
        /// </summary>
        WorkspacesEndPoint WorkspacesEndPoint();
    }
}