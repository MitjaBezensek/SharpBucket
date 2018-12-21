using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class SSH
    {
        public int? pk { get; set; }
        public string key { get; set; }
        public string label { get; set; }
    }
}