using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositoriesOverview
    {
        public List<EventData> updated { get; set; }
        public List<EventData> viewed { get; set; }
    }
}