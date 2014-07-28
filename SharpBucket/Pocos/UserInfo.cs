using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class UserInfo{
        public List<Repository> repositories { get; set; }
        public User user { get; set; }
    }
}