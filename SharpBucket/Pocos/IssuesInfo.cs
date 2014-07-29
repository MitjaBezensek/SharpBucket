using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class IssuesInfo{
        public int? Count { get; set; }
        public Filter Filter { get; set; }
        public object Search { get; set; }
        public List<Issue> Issues { get; set; }
    }
}