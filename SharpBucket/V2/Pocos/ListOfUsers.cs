using System.Collections.Generic;

namespace SharpBucket.V2.Pocos{
    public class ListOfUsers{
        public int? pagelen { get; set; }
        public List<User> values { get; set; }
        public int? page { get; set; }
        public int? size { get; set; }
    }
}