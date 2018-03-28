namespace SharpBucket.V2.Pocos
{
    public class Branch
    {
        public string message { get; set; }
        public string author { get; set; }
        public string parents { get; set; }
        public string files { get; set; }
        public string branch { get; set; }

        public string type { get; set; }
        public string path { get; set; }
        public string name { get; set; }
        public Links links { get; set; }
    }
}