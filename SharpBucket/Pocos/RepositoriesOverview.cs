using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class RepositoriesOverview{
        public List<Event> updated { get; set; }
        public List<Event> viewed { get; set; }
    }
}