namespace SharpBucket.V1.Pocos
{
    public class Revision
    {
        public string node { get; set; }
        public string path { get; set; }
        public string data { get; set; }
        public int size { get; set; }
    }
}