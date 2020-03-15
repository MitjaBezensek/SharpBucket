namespace SharpBucket.V2
{
    /// <summary>
    /// Interface that expose the methods that an end point V2 could use to perform calls to the BitBucket API.
    /// It's distinct from <see cref="ISharpBucketRequester"/> just to allow existance of extension methods
    /// specific to V2 of the API.
    /// </summary>
    public interface ISharpBucketRequesterV2 : ISharpBucketRequester
    {
    }
}