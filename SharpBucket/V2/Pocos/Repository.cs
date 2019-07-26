namespace SharpBucket.V2.Pocos
{
    public class Repository
    {
        public string scm { get; set; }
        public bool? has_wiki { get; set; }
        public string description { get; set; }
        public RepositoryLinks links { get; set; }
        public NamedBranch mainbranch { get; set; }
        public string fork_policy { get; set; }
        public string language { get; set; }
        public string created_on { get; set; }
        public RepositoryInfo parent { get; set; }
        public string full_name { get; set; }
        public string slug { get; set; }
        public bool? has_issues { get; set; }
        public UserShort owner { get; set; }
        public string updated_on { get; set; }
        public ulong? size { get; set; }
        public bool? is_private { get; set; }
        public string name { get; set; }
        public string uuid { get; set; }
        public ProjectInfo project { get; set; }
        public string website { get; set; }
    }
}