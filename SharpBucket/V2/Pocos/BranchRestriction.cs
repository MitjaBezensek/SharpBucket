using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class BranchRestriction
    {
        public string kind { get; set; }
        public List<User> users { get; set; }
        public List<Link> links { get; set; }
        public string pattern { get; set; }
        public int? value { get; set; }
        public List<Group> groups { get; set; }
        public string type { get; set; }
        public int? id { get; set; }
    }
}