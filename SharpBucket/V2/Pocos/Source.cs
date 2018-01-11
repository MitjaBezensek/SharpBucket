namespace SharpBucket.V2.Pocos
{
    public class Source
    {
        public Commit commit { get; set; }
        public Repository repository { get; set; }
        public Branch branch { get; set; }
    }
}