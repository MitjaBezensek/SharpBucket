using System;

namespace SharpBucket.V2.Pocos
{
    public class TeamInfo
    {
        public string uuid { get; set; }
        public string username { get; set; }
        public string display_name { get; set; }
        public TeamLinks links { get; set; }
    }
}
