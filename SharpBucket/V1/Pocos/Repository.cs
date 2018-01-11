namespace SharpBucket.V1.Pocos
{
    public class Repository
    {
        public string scm { get; set; }
        public bool? has_wiki { get; set; }
        public string last_updated { get; set; }
        public bool? no_forks { get; set; }
        public string created_on { get; set; }
        public string owner { get; set; }
        public string logo { get; set; }
        public string email_mailinglist { get; set; }
        public bool? is_mq { get; set; }
        public ulong? size { get; set; }
        public bool? read_only { get; set; }
        public object fork_of { get; set; }
        public object mq_of { get; set; }
        public string state { get; set; }
        public string utc_created_on { get; set; }
        public string website { get; set; }
        public string description { get; set; }
        public bool? has_issues { get; set; }
        public bool? is_fork { get; set; }
        public string slug { get; set; }
        public bool? is_private { get; set; }
        public string name { get; set; }
        public string language { get; set; }
        public string utc_last_updated { get; set; }
        public bool? email_writers { get; set; }
        public bool? no_public_forks { get; set; }
        public object creator { get; set; }
        public string resource_uri { get; set; }
    }
}