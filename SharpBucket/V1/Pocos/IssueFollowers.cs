using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class IssueFollowers
    {
        public int Count { get; set; }
        public List<User> Followers { get; set; }
    }
}