using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class Commit
    {
        public string hash { get; set; }
        public Links links { get; set; }
        public Repository repository { get; set; }
        public Author author { get; set; }
        public List<UserRole> participants { get; set; }
        public List<CommitInfo> parents { get; set; }
        public string date { get; set; }
        public string message { get; set; }
        public Rendered summary { get; set; }
    }
}