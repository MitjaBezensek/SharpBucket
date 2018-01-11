namespace SharpBucket.V2.Pocos
{
    public class Activity
    {
        public Update update { get; set; }
        public PullRequest pull_request { get; set; }
        public Comment comment { get; set; }
    }
}