using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class Links
    {
        public Link watchers { get; set; }
        public Link branches { get; set; }
        public Link tags { get; set; }
        public Link commits { get; set; }
        public List<Link> clone { get; set; }
        public Link self { get; set; }
        public Link source { get; set; }
        public Link html { get; set; }
        public Link avatar { get; set; }
        public Link hooks { get; set; }
        public Link forks { get; set; }
        public Link downloads { get; set; }
        public Link isues { get; set; }
        public Link pullrequests { get; set; }
        public Link repositories { get; set; }
        public Link link { get; set; }
        public Link followers { get; set; }
        public Link following { get; set; }
    }
}