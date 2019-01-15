using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class TreeEntry
    {
        public string Type { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
        public List<string> Attributes { get; set; }
        public TreeEntryLinks Links { get; set; }
        public Commit Commit { get; set; }
    }
}
