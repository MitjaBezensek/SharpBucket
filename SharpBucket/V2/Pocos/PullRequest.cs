using System;
using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    public class PullRequest
    {
        public string description { get; set; }
        public PullRequestLinks links { get; set; }
        public User author { get; set; }
        public bool? close_source_branch { get; set; }
        public string title { get; set; }
        public Source destination { get; set; }
        public string reason { get; set; }
        public object closed_by { get; set; }
        public Source source { get; set; }
        public string state { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public object merge_commit { get; set; }
        public int? id { get; set; }
        public List<User> Reviewers { get; set; }
        public List<UserRole> Participants { get; set; }
    }
}