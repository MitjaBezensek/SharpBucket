namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed to the commits resource to filter the
    /// returned list of commits.
    /// </summary>
    public class ListCommitsParameters : CommitsParameters
    {
        /// <summary>
        /// The maximum number of items to return. 0 returns all items.
        /// </summary>
        public int Max { get; set; }
    }
}
