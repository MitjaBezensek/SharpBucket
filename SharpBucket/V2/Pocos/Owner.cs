using System;

namespace SharpBucket.V2.Pocos
{
    public class Owner
    {
        [Obsolete("Expected to be removed on 29 April 2019 for GDPR concerns")]
        public string username { get; set; }
        public string display_name { get; set; }
        public Links links { get; set; }
    }
}