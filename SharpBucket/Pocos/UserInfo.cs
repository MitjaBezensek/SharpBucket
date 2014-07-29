using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class UserInfo{
        public List<Repository> Repositories { get; set; }
        public User User { get; set; }
    }
}