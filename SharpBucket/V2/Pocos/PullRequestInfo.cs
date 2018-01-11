namespace SharpBucket.V2.Pocos
{
    public class PullRequestInfo
    {
        public string role { get; set; }
        public User user { get; set; }
        public bool? approved { get; set; }
    }
}