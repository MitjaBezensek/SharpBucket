namespace SharpBucket.V1.Pocos{
    public class Comment{
        public string Content { get; set; }
        public User Author_info { get; set; }
        public int? Comment_id { get; set; }
        public string Utc_updated_on { get; set; }
        public string Utc_created_on { get; set; }
        public bool? Is_spam { get; set; }
    }
}