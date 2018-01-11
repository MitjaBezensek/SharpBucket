using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class UserInfo
    {
        public List<Repository> repositories { get; set; }
        public User user { get; set; }
    }
}