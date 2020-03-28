using System;

namespace SharpBucket.V2.Pocos
{
    public class Issue
    {
        public int id { get; set; }

        public IssuePriority priority { get; set; }

        public IssueKind kind { get; set; }

        public Repository repository { get; set; }

        public IssueLinks links { get; set; }

        public User reporter { get; set; }

        public string title { get; set; }

        public object component { get; set; }

        public int votes { get; set; }

        public int watches { get; set; }

        public Rendered content { get; set; }

        public User assignee { get; set; }

        public IssueStatus state { get; set; }

        public object version { get; set; }

        public DateTime createdOn { get; set; }

        public DateTime? updatedOn { get; set; }

        public DateTime? editedOn { get; set; }

        public object milestone { get; set; }
    }
}
