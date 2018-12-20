using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class ChangesetInfo
    {
        public int? count { get; set; }
        public object start { get; set; }
        public int? limit { get; set; }
        public List<Changeset> changesets { get; set; }
    }
}