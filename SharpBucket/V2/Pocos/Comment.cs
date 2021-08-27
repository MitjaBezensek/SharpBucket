namespace SharpBucket.V2.Pocos
{
    public class Comment
    {
        public CommentInfo parent { get; set; }
        public CommentLinks links { get; set; }
        public Rendered content { get; set; }
        public string created_on { get; set; }
        public UserInfo user { get; set; }
        public string updated_on { get; set; }
        public int? id { get; set; }
        public bool? deleted { get; set; }
        public Location inline { get; set; }
        public string type { get; set; }
    }
}