namespace SharpBucket.V1.Pocos
{
    public class Comment
    {
        public string content { get; set; }
        public User author_info { get; set; }
        public int? comment_id { get; set; }
        public string utc_updated_on { get; set; }
        public string utc_created_on { get; set; }
        public bool? is_spam { get; set; }
    }
}