using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Link
    {
        public LinkInfo handler { get; set; }
        public int? id { get; set; }
    }
}