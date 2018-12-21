using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Consumer
    {
        public string description { get; set; }
        public int? id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string secret { get; set; }
        public string url { get; set; }
    }
}