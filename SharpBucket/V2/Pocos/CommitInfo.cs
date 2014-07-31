using System.Collections.Generic;

namespace SharpBucket.V2.Pocos{
    public class CommitInfo {
        public int pagelen { get; set; }
        public List<Commit> values { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}