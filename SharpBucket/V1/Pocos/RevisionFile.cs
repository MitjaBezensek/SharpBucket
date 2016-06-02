namespace SharpBucket.V1.Pocos
{
    public class RevisionFile
    {
        public string path { get; set; }
        public string revision { get; set; }
        public long size { get; set; }
        public string timestamp { get; set; }
        public string utctimestamp { get; set; }
    }
}
