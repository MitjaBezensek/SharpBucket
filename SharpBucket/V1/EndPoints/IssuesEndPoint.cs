using System.Collections.Generic;
using SharpBucket.V1.Pocos;

namespace SharpBucket.V1.EndPoints{
    public class IssuesEndPoint{
        private readonly RepositoriesEndPointV1 _repositoriesEndPointV1;

        public IssueEndPoint Issue(int issueId){
            return new IssueEndPoint(_repositoriesEndPointV1, issueId);
        }

        public IssuesEndPoint(RepositoriesEndPointV1 repositoriesEndPointV1){
            _repositoriesEndPointV1 = repositoriesEndPointV1;
        }

        public IssuesInfo ListIssues(){
            return _repositoriesEndPointV1.ListIssues();
        }

        public Issue PostIssue(Issue issue){
            return _repositoriesEndPointV1.PostIssue(issue);
        }

        public Issue GetIssue(int? issueId){
            return _repositoriesEndPointV1.GetIssue(issueId);
        }

        public Issue PutIssue(Issue issue){
            return _repositoriesEndPointV1.PutIssue(issue);
        }

        public Issue DeleteIssue(Issue issue){
            return _repositoriesEndPointV1.DeleteIssue(issue);
        }

        public Issue DeleteIssue(int? issueId){
            return _repositoriesEndPointV1.DeleteIssue(issueId);
        }

        public IssueFollowers ListIssueFollowers(Issue issue){
            return _repositoriesEndPointV1.ListIssueFollowers(issue);
        }

        public IssueFollowers ListIssueFollowers(int? issueId){
            return _repositoriesEndPointV1.ListIssueFollowers(issueId);
        }

        public List<Comment> ListIssueComments(Issue issue){
            return _repositoriesEndPointV1.ListIssueComments(issue);
        }

        public List<Comment> ListIssueComments(int issueId){
            return _repositoriesEndPointV1.ListIssueComments(issueId);
        }

        public Comment PostIssueComment(int issueId, Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(issueId, comment);
        }

        public Comment PostIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.PostIssueComment(issue, comment);
        }

        public Comment GetIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issue, commentId);
        }

        public Comment GetIssueComment(int issueId, int? commentId){
            return _repositoriesEndPointV1.GetIssueComment(issueId, commentId);
        }

        public Comment PutIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(issue, comment);
        }

        public Comment PutIssueComment(int issudId, Comment comment){
            return _repositoriesEndPointV1.PutIssueComment(issudId, comment);
        }

        public Comment DeleteIssueComment(Issue issue, Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(issue, comment);
        }

        public Comment DeleteIssueComment(Issue issue, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issue, commentId);
        }

        public Comment DeleteIssueComment(int? issueId, int? commentId){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, commentId);
        }

        public Comment DeleteIssueComment(int? issueId, Comment comment){
            return _repositoriesEndPointV1.DeleteIssueComment(issueId, comment);
        }

        public List<Component> ListComponents(){
            return _repositoriesEndPointV1.ListComponents();
        }

        public Component PostComponent(Component component){
            return _repositoriesEndPointV1.PostComponent(component);
        }

        public Component GetComponent(int? componentId){
            return _repositoriesEndPointV1.GetComponent(componentId);
        }

        public Component PutComponent(Component component){
            return _repositoriesEndPointV1.PutComponent(component);
        }

        public Component DeleteComponent(Component component){
            return _repositoriesEndPointV1.DeleteComponent(component);
        }

        public Component DeleteComponent(int? componentId){
            return _repositoriesEndPointV1.DeleteComponent(componentId);
        }

        public List<Milestone> ListMilestones(){
            return _repositoriesEndPointV1.ListMilestones();
        }

        public Milestone PostMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PostMilestone(milestone);
        }

        public Milestone GetMilestone(int? milestoneId){
            return _repositoriesEndPointV1.GetMilestone(milestoneId);
        }

        public Milestone PutMilestone(Milestone milestone){
            return _repositoriesEndPointV1.PutMilestone(milestone);
        }

        public Milestone DeleteMilestone(Milestone milestone){
            return _repositoriesEndPointV1.DeleteMilestone(milestone);
        }

        public Milestone DeleteMilestone(int? milestoneId){
            return _repositoriesEndPointV1.DeleteMilestone(milestoneId);
        }

        public List<Version> ListVersions(){
            return _repositoriesEndPointV1.ListVersions();
        }

        public Version PostVersion(Version version){
            return _repositoriesEndPointV1.PostVersion(version);
        }

        public Version GetVersion(int? versionId){
            return _repositoriesEndPointV1.GetVersion(versionId);
        }

        public Version PutVersion(Version version){
            return _repositoriesEndPointV1.PutVersion(version);
        }

        public Version DeleteVersion(Version version){
            return _repositoriesEndPointV1.DeleteVersion(version);
        }

        public Version DeleteVersion(int? versionId){
            return _repositoriesEndPointV1.DeleteVersion(versionId);
        }
    }
}