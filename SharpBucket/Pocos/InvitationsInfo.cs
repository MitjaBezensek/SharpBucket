using System.Collections.Generic;

namespace SharpBucket.POCOs{
    public class InvitationsInfo {
        public List<string> Groups { get; set; }
        public User Invited_by { get; set; }
        public string Utc_sent_on { get; set; }
        public string Email { get; set; }
    }
}