using System;

namespace SharpBucket.V2.Pocos
{
    public class WorkspaceInfo
    {
        public string type { get; set; }

        public Guid uuid { get; set; }

        public string name { get; set; }

        public string slug { get; set; }

        public WorkspaceInfoLinks links { get; set; }
    }
}
