using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Revision
    {
        public string node { get; set; }
        public string path { get; set; }
        public string data { get; set; }
        public int size { get; set; }
    }
}