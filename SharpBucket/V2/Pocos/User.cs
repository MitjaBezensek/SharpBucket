using System;

namespace SharpBucket.V2.Pocos
{
    public class User
    {
        public string account_id { get; set; }

        public string uuid { get; set; }

        [Obsolete("Expected to be removed on 29 April 2019 for GDPR concerns")]
        public string username { get; set; }

        public string nickname { get; set; }

        /// <summary>
        /// team or user
        /// </summary>
        public string type { get; set; }

        [Obsolete("Expected to be removed on 29 April 2019 for GDPR concerns")]
        public string website { get; set; }

        public string display_name { get; set; }

        public Links links { get; set; }

        public string created_on { get; set; }

        [Obsolete("Expected to be removed on 29 April 2019 for GDPR concerns")]
        public string location { get; set; }

        /// <summary>
        /// active, inactive or closed
        /// </summary>
        public string account_status { get; set; }
    }
}