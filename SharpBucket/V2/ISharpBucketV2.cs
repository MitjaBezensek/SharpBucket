namespace SharpBucket.V2
{
    /// <summary>
    /// Interface that expose all the methods available on the <see cref="SharpBucketV2"/> class.
    /// This interface should be used for mocking <see cref="SharpBucketV2"/>.
    /// </summary>
    public interface ISharpBucketV2 : ISharpBucket, ISharpBucketRequesterV2
    {
    }
}