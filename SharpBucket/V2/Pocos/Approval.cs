using System;

namespace SharpBucket.V2.Pocos
{
    public class Approval
    {
        public DateTimeOffset date { get; set; }
        public PullRequest pullrequest { get; set; }
        public User user { get; set; }
    }
}
