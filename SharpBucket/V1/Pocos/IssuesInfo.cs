using System.Collections.Generic;

namespace SharpBucket.V1.Pocos
{
    public class IssuesInfo
    {
        public int? count { get; set; }
        public Filter filter { get; set; }
        public object search { get; set; }
        public List<Issue> issues { get; set; }
    }
}