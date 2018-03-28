using System;

namespace SharpBucket.V2.Pocos
{
    public class Project
    {
        public string uuid { get; set; }
        public Links links { get; set; }
        public string description { get; set; }
        public DateTime? created_on { get; set; }
        public string key { get; set; }
        public Owner owner { get; set; }
        public DateTime? updated_on { get; set; }
        public string type { get; set; }
        public bool? is_private { get; set; }
        public string name { get; set; }
    }
}
