using System;

namespace SharpBucket.V2.Pocos
{
    public class User
    {
        public string created_on;
        public string username { get; set; }
        public string type { get; set; }
        public string display_name { get; set; }
        public Links links { get; set; }
        public string uuid { get; set; }
    }
}