using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class SrcDirectory
    {
        public string node { get; set; }
        public string path { get; set; }
        public List<string> directories { get; set; }
        public List<SrcFileInfo> files { get; set; }
    }
}