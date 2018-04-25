using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{
    public class Group
    {
        public string name { get; set; }
        public Links links { get; set; }
        public string account_privilege { get; set; }
        public string full_slug { get; set; }
        public Owner owner { get; set; }
        public string type { get; set; }
        public string slug { get; set; }
    }
}
