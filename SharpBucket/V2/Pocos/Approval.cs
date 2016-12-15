using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBucket.V2.Pocos
{
    public class Approval
    {
        public DateTimeOffset date { get; set; }
        public UserShort user { get; set; }
    }
}
