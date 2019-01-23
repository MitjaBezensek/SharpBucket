namespace SharpBucket.V2.Pocos
{
    public class SrcDirectory : ISrc
    {
        public string path { get; set; }
        public SrcDirectoryLinks links { get; set; }
        public CommitInfo commit { get; set; }
    }
}
