namespace SharpBucket.V2.Pocos
{
    public class WorkspaceMembership
    {
        public string type { get; set; }

        public UserInfo user { get; set; }

        public WorkspaceInfo workspace { get; set; }

        public WorkspaceMembershipLinks links { get; set; }
    }
}
