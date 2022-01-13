namespace SharpBucket.V2.Pocos
{
    public class Tag
    {
        public string name { get; set; }
        public Commit target { get; set; }
        public Author tagger { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public TagLinks links { get; set; }
    }
}