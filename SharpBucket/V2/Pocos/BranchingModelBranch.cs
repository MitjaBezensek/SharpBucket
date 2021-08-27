namespace SharpBucket.V2.Pocos
{
    public class BranchingModelBranch
    {
        public string name { get; set; }
        public Branch branch { get; set; }
        public bool use_mainbranch { get; set; }
    }
}