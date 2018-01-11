namespace SharpBucket.V1.Pocos
{
    public class Issue
    {
        public string title { get; set; }
        public string content { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public User reported_by { get; set; }
        public User responsible { get; set; }
        public string kind { get; set; }
        public string utc_last_updated { get; set; }
        public int? comment_count { get; set; }
        public Metadata metadata { get; set; }
        public string created_on { get; set; }
        public int? local_id { get; set; }
        public int? follower_count { get; set; }
        public string utc_created_on { get; set; }
        public string resource_uri { get; set; }
        public bool? is_spam { get; set; }
    }
}