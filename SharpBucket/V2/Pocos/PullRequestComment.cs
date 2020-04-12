namespace SharpBucket.V2.Pocos
{
    public class PullRequestComment : Comment
    {
        public PullRequestInfo pullrequest { get; set; }
    }
}
