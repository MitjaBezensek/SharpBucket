namespace SharpBucket.V2.Pocos
{
    public class RepositoryCreationParameters
    {
        public string scm { get; set; }
        public string name { get; set; }
        public bool is_private { get; set; }
        public string description { get; set; }
        public string fork_policy { get; set; }
        public string language { get; set; }
        public bool has_issues { get; set; }
        public bool has_wiki { get; set; }
    }
}