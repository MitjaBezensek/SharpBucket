using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class EventInfo
    {
        public int? count { get; set; }
        public List<EventData> events { get; set; }
    }
}