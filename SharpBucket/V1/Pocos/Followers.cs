using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class Followers
    {
        public int? count { get; set; }
        public List<User> followers { get; set; }
    }
}