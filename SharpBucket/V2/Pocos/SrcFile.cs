using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class SrcFile : ISrc
    {
        public string path { get; set; }
        public SrcFileLinks links { get; set; }
        public CommitInfo commit { get; set; }
        public int size { get; set; }
        public string mimetype { get; set; }
        public List<string> attributes { get; set; }
    }
}
