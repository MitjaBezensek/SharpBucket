namespace SharpBucket.V2.EndPoints
{
    public class EnumerateCommitsParameters : CommitsParameters
    {
        /// <summary>
        /// The length of a page. If not defined the default page length will be used.
        /// </summary>
        public int? PageLen { get; set; }
    }
}
