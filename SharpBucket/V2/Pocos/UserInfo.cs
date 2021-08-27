namespace SharpBucket.V2.Pocos
{
    public class UserInfo
    {
        /// <summary>
        /// Could be user or team.
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

        /// <summary>
        /// Only for users of type user.
        /// </summary>
        public string nickname { get; set; }

        public string display_name { get; set; }

        public UserInfoLinks links { get; set; }
    }
}
