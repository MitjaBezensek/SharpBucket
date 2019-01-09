using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Privileges
    {
        public Dictionary<string, string> teams { get; set; }
    }
}