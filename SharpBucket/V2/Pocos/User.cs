namespace SharpBucket.V2.Pocos
{
    public class User
    {
        /// <summary>
        /// team or user
        /// </summary>
        public string type { get; set; }

        public string uuid { get; set; }

        /// <summary>
        /// Only for users of type user.
        /// users of type team still use username.
        /// </summary>
        public string account_id { get; set; }

        /// <summary>
        /// Removed on 29 April 2019 for GDPR concerns for users of type user.
        /// Use nickname, display_name, uuid or account_id instead.
        /// For users of type team this is still available.
        /// </summary>
        public string username { get; set; }

        public string nickname { get; set; }

        public string display_name { get; set; }

        public Links links { get; set; }

        public string created_on { get; set; }

        /// <summary>
        /// active, inactive or closed
        /// </summary>
        public string account_status { get; set; }
    }
}