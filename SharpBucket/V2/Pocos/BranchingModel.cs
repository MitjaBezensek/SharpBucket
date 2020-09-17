using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class BranchingModel
    {
        public NamedBranchingModelBranch development { get; set; }
        public NamedBranchingModelBranch production { get; set; }
        public List<BranchType> branch_types { get; set; }
        public string type { get; set; }
        public Links links { get; set; }
    }
}