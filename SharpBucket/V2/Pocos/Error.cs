using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// The standardize error embedded in <see cref="ErrorResponse"/>
    /// </summary>
    public class Error
    {
        public string message { get; set; }
        public Dictionary<string, string[]> fields { get; set; }
        public string detail { get; set; }
        public string id { get; set; }
        public Dictionary<string, string> data { get; set; }
    }
}
