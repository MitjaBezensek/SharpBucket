using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{
    public class IssueLinks
    {
        public Link attachments { get; set; }
        public Link self { get; set; }
        public Link watch { get; set; }
        public Link comments { get; set; }
        public Link html { get; set; }
        public Link vote { get; set; }
    }
}
