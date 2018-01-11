using System;

namespace SharpBucket.V1.Pocos
{
    public class SrcFileInfo
    {
        public long size { get; set; }
        public string path { get; set; }
        public DateTime timestamp { get; set; }
        public string revision { get; set; }
    }
}