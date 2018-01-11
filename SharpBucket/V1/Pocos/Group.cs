using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class Group
    {
        public string name { get; set; }
        public string permission { get; set; }
        public string auto_add { get; set; }
        public List<User> members { get; set; }
        public User owner { get; set; }
        public string slug { get; set; }
    }
}