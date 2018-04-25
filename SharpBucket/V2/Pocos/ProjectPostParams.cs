using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{
    public class ProjectPostParams
    {
        public string description { get; set; }
        public string key { get; set; }
        public bool? is_private { get; set; }
        public string name { get; set; }
    }
}
