using System.Collections.Generic;

namespace SharpBucket.V1.Pocos{
    public class UserInfo{
        public List<Repository> Repositories { get; set; }
        public User User { get; set; }
    }
}