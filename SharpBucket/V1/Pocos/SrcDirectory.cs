using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class SrcDirectory
    {
        public string node { get; set; }
        public string path { get; set; }
        public List<string> directories { get; set; }
        public List<SrcFileInfo> files { get; set; }
    }
}