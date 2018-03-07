namespace SharpBucket.V1.Pocos
{
    public class Link
    {
        public LinkInfo handler { get; set; }
        public int? id { get; set; }
    }

    public class Link2 : Link
    {
        public new string handler { get; set; }
        public string link_url { get; set; }
        public string link_key { get; set; }
    }
}