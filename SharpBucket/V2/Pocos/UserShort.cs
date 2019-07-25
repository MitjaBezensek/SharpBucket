using System;

namespace SharpBucket.V2.Pocos
{
    public class UserShort
    {
        public string account_id { get; set; }

        public string uuid { get; set; }

        [Obsolete("Expected to be removed on 29 April 2019 for GDPR concerns. Use nickname, display_name, uuid or account_id instead.")]
        public string username { get; set; }

        public string nickname { get; set; }

        /// <summary>
        /// team or user
        /// </summary>
        public string type { get; set; }

        public string display_name { get; set; }
        
        public Links links { get; set; }
    }
}