using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class Comment
    {
        public string content { get; set; }
        public User author_info { get; set; }
        public int? comment_id { get; set; }
        public string utc_updated_on { get; set; }
        public string utc_created_on { get; set; }
        public bool? is_spam { get; set; }
    }
}