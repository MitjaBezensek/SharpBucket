using System;
using System.Collections.Generic;


namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// Base class for endpoints and resources.
    /// </summary>
    /// <remarks>
    /// EndPoint classes are just like Resources classes.
    /// The only difference is that they are at the root of the bitbucket API.
    /// </remarks>
    public abstract class EndPoint
    {
        /// <summary>
        /// The requester used to create this <see cref="EndPoint"/>
        /// </summary>
        protected ISharpBucketRequesterV2 SharpBucketV2 { get; }

        /// <summary>
        /// the base url of this end point.
        /// It's a relative URL from the base url of the <see cref="ISharpBucketRequesterV2"/>.
        /// It is formatted without any slash at the beginning or the end.
        /// </summary>
        protected string BaseUrl { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="EndPoint"/> from scratch.
        /// This is the constructor to use for EndPoint classes.
        /// </summary>
        protected EndPoint(ISharpBucketRequesterV2 sharpBucketV2, string resourcePath)
        {
            this.SharpBucketV2 = sharpBucketV2 ?? throw new ArgumentNullException(nameof(sharpBucketV2));
            BaseUrl = resourcePath.Trim('/');
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EndPoint"/> from a parent <see cref="EndPoint"/>.
        /// This is the constructor to use for Resources classes.
        /// </summary>
        protected EndPoint(EndPoint parentEndPoint, string resourcePathFromParent)
        {
            SharpBucketV2 = parentEndPoint.SharpBucketV2;
            BaseUrl = parentEndPoint.BaseUrl + "/" + resourcePathFromParent.Trim('/');
        }

        /// <summary>
        /// Returns a list of paginated values.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="overrideUrl">The override URL.</param>
        /// <param name="max">Set to 0 for unlimited size.</param>
        /// <param name="requestParameters"></param>
        /// <returns></returns>
        /// <exception cref="BitbucketV2Exception">Thrown when the server fails to respond.</exception>
        protected List<TValue> GetPaginatedValues<TValue>(string overrideUrl, int max = 0, IDictionary<string, object> requestParameters = null)
        {
            return SharpBucketV2.GetPaginatedValues<TValue>(overrideUrl, max, requestParameters);
        }
    }
}