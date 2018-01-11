namespace SharpBucket.V1.Pocos
{
    public class User
    {
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string display_name { get; set; }
        public bool? is_staff { get; set; }
        public string avatar { get; set; }
        public string resource_uri { get; set; }
        public bool? is_team { get; set; }
    }
}