using System.Collections.Generic;

namespace SharpBucket.V1.Pocos{
    public class Tag{
        public string Node { get; set; }
        public List<FileInfo> Files { get; set; }
        public List<BranchInfo> Branches { get; set; }
        public string Raw_author { get; set; }
        public string Utctimestamp { get; set; }
        public string Author { get; set; }
        public string Timestamp { get; set; }
        public string Raw_node { get; set; }
        public List<BranchInfo> Parents { get; set; }
        public object Branch { get; set; }
        public string Message { get; set; }
        public object Revision { get; set; }
        public int? Size { get; set; }
    }
}