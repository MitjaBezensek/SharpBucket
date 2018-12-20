using System;

namespace SharpBucket.V2.Pocos
{
    public class BuildInfo
    {
        public string name { get; set; }
        public string url { get; set; }
        public string refname { get; set; }

        public BuildInfoState state { get; set; }

        public string key { get; set; }
        public DateTime updated_on { get; set; }
        public DateTime created_on { get; set; }
        public string description { get; set; }
    }

    public enum BuildInfoState
    {
        SUCCESSFUL,
        FAILED,
        INPROGRESS,
        STOPPED
    }
}