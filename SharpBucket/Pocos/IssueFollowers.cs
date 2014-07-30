using System.Collections.Generic;
using SharpBucket.POCOs;

namespace SharpBucket.Pocos{
    public class IssueFollowers{
        public int Count { get; set; }
        public List<User> Followers { get; set; }
    }
}