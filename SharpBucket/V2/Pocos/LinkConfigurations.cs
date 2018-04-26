using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{
    public class LinkConfigurations : Link
    {
        public string handler { get; set; }
        public string link_url { get; set; }
        public string link_key { get; set; }
    }
}
