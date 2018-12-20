using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class UserInfo
    {
        public List<Repository> repositories { get; set; }
        public User user { get; set; }
    }
}