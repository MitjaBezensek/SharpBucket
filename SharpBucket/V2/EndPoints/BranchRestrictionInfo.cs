using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints{
    public class BranchRestrictionInfo {
        public int page { get; set; }
        public int pagelen { get; set; }
        public int size { get; set; }
        public List<BranchRestriction> values { get; set; }
    }
}