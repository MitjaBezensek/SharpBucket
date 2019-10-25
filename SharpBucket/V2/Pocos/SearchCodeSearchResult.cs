using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class SearchCodeSearchResult
    {
        public string type { get; set; }
        public int content_match_count { get; set; }
        public List<SearchContentMatch> content_matches { get; set; }
        public List<SearchSegment> path_matches { get; set; }
        public SrcFileInfo file { get; set; }
    }
}
