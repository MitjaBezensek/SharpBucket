using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class RepositoryLinks
    {
        public Link avatar { get; set; }
        public Link branches { get; set; }
        public List<NamedLink> clone { get; set; }
        public Link commits { get; set; }
        public Link downloads { get; set; }
        public Link forks { get; set; }
        public Link hooks { get; set; }
        public Link html { get; set; }
        public Link pullrequests { get; set; }
        public Link self { get; set; }
        public Link source { get; set; }
        public Link tags { get; set; }
        public Link watchers { get; set; }
    }
}
