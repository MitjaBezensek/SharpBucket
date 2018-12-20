using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Component
    {
        public string name { get; set; }
        public int? id { get; set; }
    }
}