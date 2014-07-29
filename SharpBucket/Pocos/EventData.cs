namespace SharpBucket.POCOs{
    public class EventData {
        public object Node { get; set; }
        public object Description { get; set; }
        public Repository Repository { get; set; }
        public string Created_on { get; set; }
        public User User { get; set; }
        public string Utc_created_on { get; set; }
        public string @Event { get; set; }
    }
}