namespace SharpBucket.V2.Pocos
{
    public class Email
    {
        public bool is_primary { get; set; }
        public bool is_confirmed { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public Links links { get; set; }
    }
}