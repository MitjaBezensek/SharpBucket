using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class RepositoriesOverview{
        public List<EventData> Updated { get; set; }
        public List<EventData> Viewed { get; set; }
    }
}