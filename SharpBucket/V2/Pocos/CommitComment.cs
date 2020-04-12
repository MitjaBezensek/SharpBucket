namespace SharpBucket.V2.Pocos
{
    public class CommitComment : Comment
    {
        public CommitInfo commit { get; set; }
    }
}
