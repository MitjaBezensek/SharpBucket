namespace SharpBucket.V2.Pocos
{
    public class Branch
    {
        public string name { get; set; }
        public Commit target { get; set; }
        public MergeStrategy default_merge_strategy { get; set; }
        public BranchLinks links { get; set; }
    }
}