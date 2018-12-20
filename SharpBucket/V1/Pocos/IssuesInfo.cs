using System;
using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class IssuesInfo
    {
        public int? count { get; set; }
        public Filter filter { get; set; }
        public object search { get; set; }
        public List<Issue> issues { get; set; }
    }
}