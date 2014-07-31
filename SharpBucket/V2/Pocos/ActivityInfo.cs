using System.Collections.Generic;

namespace SharpBucket.V2.Pocos{
    public class ActivityInfo{
        public int? pagelen { get; set; }
        public string next { get; set; }
        public List<Activity> values { get; set; }
        public int? page { get; set; }
        public int? size { get; set; }
    }
}