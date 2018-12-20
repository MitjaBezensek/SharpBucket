using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Metadata
    {
        public string kind { get; set; }
        public object version { get; set; }
        public object component { get; set; }
        public object milestone { get; set; }
    }
}