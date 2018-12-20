using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class SrcFileInfo
    {
        public long size { get; set; }
        public string path { get; set; }
        public DateTime timestamp { get; set; }
        public string revision { get; set; }
    }
}