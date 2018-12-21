using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositoryEvent
    {
        public string name { get; set; }
        public string url { get; set; }
        public string avatar { get; set; }
        public string owner { get; set; }
        public string slug { get; set; }
        public bool? is_private { get; set; }
    }
}