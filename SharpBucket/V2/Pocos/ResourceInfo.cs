using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBucket.V2.Pocos {
    public class ResourceInfo<T> {
        public string next { get; set; }
        public int? page { get; set; }
        public int? pagelen { get; set; }
        public int? size { get; set; }
        public List<T> values { get; set; }
    }
}
