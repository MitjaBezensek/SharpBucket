using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class Branch{
        public string node { get; set; }
        public List<File> files { get; set; }
        public string raw_author { get; set; }
        public string utctimestamp { get; set; }
        public string author { get; set; }
        public string timestamp { get; set; }
        public string raw_node { get; set; }
        public List<string> parents { get; set; }
        public string branch { get; set; }
        public string message { get; set; }
        public object revision { get; set; }
        public int? size { get; set; }
    }
}