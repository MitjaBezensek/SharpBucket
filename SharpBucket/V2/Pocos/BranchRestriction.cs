using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class BranchRestriction
    {
        public List<Group> groups { get; set; }
        public int? id { get; set; }
        public string kind { get; set; }
        public List<Link> links { get; set; }
        public string pattern { get; set; }
        public List<User> users { get; set; }
        public int? value { get; set; }
        public string type { get; set; }
    }
}