namespace SharpBucket.V2.Pocos
{
    public class NamedBranchingModelBranch
    {
        public string name { get; set; }
        public BranchingModelBranch branch { get; set; }
        public bool use_mainbranch { get; set; }
    }
}