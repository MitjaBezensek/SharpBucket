using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBucket.V1.Pocos
{
    public class IssueSearchParameters
    {
        public int? limit { get; set; }
        public int? start { get; set; }
        public string search { get; set; }
        public string sort { get; set; }
        public string content { get; set; }
        public string version { get; set; }
        public string milestone { get; set; }
        public string kind { get; set; }
        public string status { get; set; }
        public string responsible { get; set; }
        public string reported_by { get; set; }
    }
}