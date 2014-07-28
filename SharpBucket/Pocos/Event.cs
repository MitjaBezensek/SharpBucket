namespace SharpBucket.POCOs{
    public class Event {
        public object node { get; set; }
        public object description { get; set; }
        public Repository repository { get; set; }
        public string created_on { get; set; }
        public User user { get; set; }
        public string utc_created_on { get; set; }
        public string @event { get; set; }
    }
}