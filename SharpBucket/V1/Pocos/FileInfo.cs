using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class FileInfo
    {
        public string type { get; set; }
        public string file { get; set; }
    }
}