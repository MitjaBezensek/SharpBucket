using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class EventInfo
    {
        public int? count { get; set; }
        public List<EventData> events { get; set; }
    }
}