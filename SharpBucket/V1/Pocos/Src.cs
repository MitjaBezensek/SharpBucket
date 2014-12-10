using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBucket.V1.Pocos
{
    public class Src
    {
        public string node { get; set; }
        public string path { get; set; }
        public string data { get; set; }
        public int size { get; set; }
    }

    public class Raw
    {
        public string base64data { get; set; }
    }
}
