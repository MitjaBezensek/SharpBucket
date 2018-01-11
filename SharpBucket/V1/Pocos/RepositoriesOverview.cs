using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class RepositoriesOverview
    {
        public List<EventData> updated { get; set; }
        public List<EventData> viewed { get; set; }
    }
}