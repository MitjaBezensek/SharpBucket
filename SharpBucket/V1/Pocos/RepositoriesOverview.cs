using System.Collections.Generic;

namespace SharpBucket.V1.Pocos{
    public class RepositoriesOverview{
        public List<EventData> Updated { get; set; }
        public List<EventData> Viewed { get; set; }
    }
}