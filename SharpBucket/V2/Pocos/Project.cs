namespace SharpBucket.V2.Pocos
{
    public class Project : ProjectInfo
    {
        public string description { get; set; }
        public bool is_private { get; set; }
        public TeamInfo owner { get; set; }
        public string created_on { get; set; }
        public string updated_on { get; set; }
    }
}
