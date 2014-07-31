using System.Collections.Generic;

namespace SharpBucket.V2.EndPoints {
    public class ForkInfo {
        public int pagelen { get; set; }
        public List<Fork> values { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
