using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBucket.V2.Pocos
{
    public class Email
    {
        public bool is_primary { get; set; }
        public bool is_confirmed { get; set; }
        public string type { get; set; }
        public string email { get; set; }
        public Links links { get; set; }
    }
}
