namespace SharpBucket.V2.Pocos
{
    public class BranchingModelBranch
    {
        public string type { get; set; }
        public string name { get; set; }
        public Target target { get; set; }
    }
}