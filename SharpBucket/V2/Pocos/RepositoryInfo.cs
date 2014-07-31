using System.Collections.Generic;

namespace SharpBucket.V2.Pocos {
    public class RepositoryInfo {
        public int pagelen { get; set; }
        public List<Repository> values { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
