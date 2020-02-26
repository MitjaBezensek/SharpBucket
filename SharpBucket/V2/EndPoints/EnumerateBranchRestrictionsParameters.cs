namespace SharpBucket.V2.EndPoints
{
    /// <summary>
    /// An object that can be passed on methods that enumerate branch-restrictions.
    /// </summary>
    public class EnumerateBranchRestrictionsParameters : BranchRestrictionsParameters
    {
        /// <summary>
        /// The length of a page. If not defined the default page length will be used.
        /// </summary>
        public int? PageLen { get; set; }
    }
}