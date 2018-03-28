namespace SharpBucket.V2.Pocos
{
    public class LinkPostParams : Link
    {
        public string handler { get; set; }
        public string link_url { get; set; }
        public string link_key { get; set; }
    }
}