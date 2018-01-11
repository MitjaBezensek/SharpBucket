namespace SharpBucket.V1.Pocos
{
    public class DiffstatInfo
    {
        public string type { get; set; }
        public string file { get; set; }
        public Diffstat diffstat { get; set; }
    }
}