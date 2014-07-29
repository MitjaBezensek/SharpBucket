namespace SharpBucket.POCOs{
    public class Issue{
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
        public User Reported_by { get; set; }
        public string Utc_last_updated { get; set; }
        public int? Comment_count { get; set; }
        public Metadata Metadata { get; set; }
        public string Content { get; set; }
        public string Created_on { get; set; }
        public int? Local_id { get; set; }
        public int? Follower_count { get; set; }
        public string Utc_created_on { get; set; }
        public string Resource_uri { get; set; }
        public bool? Is_spam { get; set; }
    }
}