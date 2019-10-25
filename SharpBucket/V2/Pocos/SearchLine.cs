using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class SearchLine
    {
        public int line { get; set; }
        public List<SearchSegment> segments { get; set; }
    }
}