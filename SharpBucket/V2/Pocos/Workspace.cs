using System;

namespace SharpBucket.V2.Pocos
{
    public class Workspace
    {
        public string type { get; set; }

        public Guid uuid { get; set; }

        public string name { get; set; }

        public string slug { get; set; }

        public bool is_private { get; set; }

        public WorkspaceLinks links { get; set; }

        public DateTimeOffset created_on { get; set; }
    }
}