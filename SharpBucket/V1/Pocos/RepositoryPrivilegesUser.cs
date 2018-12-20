using System;
using SharpBucket.V2.Pocos;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositoryPrivilegesUser
    {
        public string repo { get; set; }
        public string privilege { get; set; }
        public Owner user { get; set; }
    }
}