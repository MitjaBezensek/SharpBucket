using System.Collections.Generic;

namespace SharpBucket.V2.Pocos{
    public class WatcherInfo{
        public int pagelen { get; set; }
        public List<Watcher> values { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}