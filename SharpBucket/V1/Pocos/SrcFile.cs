using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class SrcFile
    {
        public string node { get; set; }
        public string path { get; set; }
        public string data { get; set; }
        public long size { get; set; }
    }
}