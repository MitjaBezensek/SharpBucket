using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Wiki
    {
        public string data { get; set; }
        public string markup { get; set; }
        public string rev { get; set; }
    }
}