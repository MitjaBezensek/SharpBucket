using SharpBucket.V2.EndPoints;

namespace SharpBucket.V2.Pocos {
    public class Destination {
        public Commit commit { get; set; }
        public Repository repository { get; set; }
        public Branch branch { get; set; }
    }
}
