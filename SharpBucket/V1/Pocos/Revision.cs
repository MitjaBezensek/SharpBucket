using System.Collections.Generic;

namespace SharpBucket.V1.Pocos {
   public class Revision {
      public string node { get; set; }
      public string path { get; set; }
      public List<RevisionFile> files { get; set; }
      public List<string> directories { get; set; }
   }
}
