using System;

namespace SharpBucket.V2.Pocos
{
    public class Project : ProjectInfo
    {
        public string description { get; set; }
        public bool is_private { get; set; }
        public bool? has_publicly_visible_repos { get; set; }
        public UserInfo owner { get; set; }
        public WorkspaceInfo workspace { get; set; }
        public DateTimeOffset? created_on { get; set; }
        public DateTimeOffset? updated_on { get; set; }
    }
}
