using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class SSHDetailed
    {
        public string comment { get; set; }
        public object added_on { get; set; }
        public User user { get; set; }
        public string key { get; set; }
        public string label { get; set; }
    }
}