using System;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositorySimple
    {
        public Owner owner { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }
}