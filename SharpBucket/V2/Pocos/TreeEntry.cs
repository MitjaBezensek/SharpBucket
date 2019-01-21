using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class TreeEntry
    {
        public string type { get; set; }
        public string path { get; set; }
        public int size { get; set; }
        public List<string> attributes { get; set; }
        public TreeEntryLinks links { get; set; }
        public CommitInfo commit { get; set; }
    }
}
