using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class EventInfo {
        public int? count { get; set; }
        public List<Event> events { get; set; }
    }
}