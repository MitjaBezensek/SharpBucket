namespace SharpBucket.V2.Pocos
{
    public class Location
    {
        /// <summary>
        /// The path of the file this comment is anchored to.
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// The comment's anchor line in the old version of the file.
        /// </summary>
        public int? from { get; set; }

        /// <summary>
        /// The comment's anchor line in the new version of the file.
        /// </summary>
        public int? to { get; set; }
    }
}