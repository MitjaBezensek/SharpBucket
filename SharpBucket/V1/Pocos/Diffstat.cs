using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Diffstat
    {
        public object removed { get; set; }
        public object added { get; set; }
    }
}