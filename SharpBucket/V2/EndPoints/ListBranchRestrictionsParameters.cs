namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that list branch-restrictions.
    /// </summary>
    public class ListBranchRestrictionsParameters : BranchRestrictionsParameters
    {
        /// <summary>
        /// The maximum number of items to return. 0 returns all items.
        /// </summary>
        public int Max { get; set; }
    }
}