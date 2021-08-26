namespace SharpBucket.V2.Pocos
{
    public class UserInfo
    {
        public string type { get; set; }

        public string uuid { get; set; }

        /// <summary>
        /// Only for users of type team.
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// Only for users of type user.
        /// </summary>
        public string account_id { get; set; }

        /// <summary>
        /// Only for users of type user.
        /// </summary>
        public string nickname { get; set; }

        public string display_name { get; set; }

        public UserInfoLinks links { get; set; }
    }
}
