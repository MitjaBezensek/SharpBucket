using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
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