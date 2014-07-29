namespace SharpBucket.POCOs{
    public class RepositoryEvent{
        public string Name { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
        public string Owner { get; set; }
        public string Slug { get; set; }
        public bool? Is_psrivate { get; set; }
    }
}